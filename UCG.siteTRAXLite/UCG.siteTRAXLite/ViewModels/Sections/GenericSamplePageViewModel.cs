using Acr.UserDialogs;
using System.Windows.Input;
using UCG.siteTRAXLite.Common.Constants;
using UCG.siteTRAXLite.Entities.SorEforms;
using UCG.siteTRAXLite.Managers.Mappers;
using UCG.siteTRAXLite.Managers.SorEformManager;
using UCG.siteTRAXLite.Models;
using UCG.siteTRAXLite.Services;

namespace UCG.siteTRAXLite.ViewModels.Sections
{
    public class GenericSamplePageViewModel : ViewModelBase
    {
        private readonly ISorEformManager _sorEformManager;

        private bool IsLoadingQuestion = false;

        public ConcurrentObservableCollection<ActionItemEntity> Questions { get; set; }
        public ConcurrentObservableCollection<ActionItemEntity> SummaryQuestions { get; set; }
        public ConcurrentObservableCollection<BreadcrumbEntity> Breadcrumbs { get; set; }
        public ConcurrentObservableCollection<PriceCodeEntity> PriceCodes { get; set; }

        private bool showQuestions;
        public bool ShowQuestions
        {
            get { return showQuestions; }
            set { SetProperty(ref showQuestions, value); }
        }

        private bool showSummary;
        public bool ShowSummary
        {
            get { return showSummary; }
            set { SetProperty(ref showSummary, value); }
        }

        private BreadcrumbEntity selectedBreadcrumb;
        public BreadcrumbEntity SelectedBreadcrumb
        {
            get { return selectedBreadcrumb; }
            set
            {
                if (value != null)
                {
                    ShowQuestions = true;
                    HandleSelectedBreadcrumb(value);
                    SetProperty(ref selectedBreadcrumb, value);
                }
            }
        }

        private bool isShowPriceCodeQTY;
        public bool IsShowPriceCodeQTY
        {
            get { return isShowPriceCodeQTY; }
            set { SetProperty(ref isShowPriceCodeQTY, value); }
        }

        private ICommand cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                return this.cancelCommand ?? (this.cancelCommand = new Command(async () => await Cancel()));
            }
        }

        private ICommand confirmCommand;

        public ICommand ConfirmCommand
        {
            get
            {
                return this.confirmCommand ?? (this.confirmCommand = new Command(async () => await Confirm()));
            }
        }

        private ICommand updateActionListCommand;

        public ICommand UpdateActionListCommand
        {
            get
            {
                return updateActionListCommand ?? (updateActionListCommand = new Command<ActionItemEntity>(async (actionItemEntity) => await UpdateActionList(actionItemEntity)));
            }
        }

        public GenericSamplePageViewModel(INavigationService navigationService,
            IAlertService alertService,
            IOpenAppService openAppService,
            ISorEformManager sorEformManager,
            IServiceEntityMapper mapper) : base(navigationService, alertService, openAppService, mapper)
        {
            _sorEformManager = sorEformManager;
            Questions = new ConcurrentObservableCollection<ActionItemEntity>();
            SummaryQuestions = new ConcurrentObservableCollection<ActionItemEntity>();
            Breadcrumbs = new ConcurrentObservableCollection<BreadcrumbEntity>();
            PriceCodes = new ConcurrentObservableCollection<PriceCodeEntity>();

            PageTitle = "Jobs";
        }

        public async override Task OnNavigatingTo(object parameter)
        {
            ClearData();
            await LoadBreadcrumbs(parameter as SectionEntity);
        }

        public async Task LoadBreadcrumbs(SectionEntity section)
        {
            if (section != null)
            {
                var breadcrumbs = new List<BreadcrumbEntity>();

                if (section.ESectionType == JobSectionType.Generic)
                {
                    breadcrumbs = await _sorEformManager.GetGenericSectionBreadcrumbs();

                    breadcrumbs.Add(new BreadcrumbEntity
                    {
                        Title = "Submit"
                    });
                }

                foreach (var breadcrumb in breadcrumbs)
                {
                    Breadcrumbs.Add(breadcrumb);
                }

                SelectedBreadcrumb = Breadcrumbs.FirstOrDefault();
                SelectedBreadcrumb.IsChecked = true;
            }
        }

        private void ClearData()
        {
            Questions.Clear();
            Breadcrumbs.Clear();
        }

        private async Task UpdateActionList(ActionItemEntity actionItemEntity)
        {
            if (actionItemEntity != null && !IsLoadingQuestion)
            {
                var currentIndex = Questions.IndexOf(actionItemEntity);

                RemoveSubList(actionItemEntity);

                var newQuestions = actionItemEntity.SubActionList
                    .Where(a => a.Condition.ResponseData.Equals(actionItemEntity?.Response?.Value, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                foreach (var action in newQuestions)
                {
                    Questions.Insert(currentIndex + 1, action);
                }
            }
        }

        public void RemoveSubList(ActionItemEntity actionItemEntity)
        {
            if (actionItemEntity.SubActionList != null && actionItemEntity.SubActionList.Any())
            {
                foreach (var action in actionItemEntity.SubActionList)
                {
                    if (action.EResponseType == SorEformsResponseType.List)
                        RemoveSubList(action);

                    if (!Questions.Contains(action))
                        continue;

                    action.Response.Value = null;

                    Questions.Remove(action);
                }
            }
        }

        private void SetLevels(List<ActionItemEntity> Questions, int level = 0)
        {
            foreach (var question in Questions)
            {
                question.Level = level;

                if (question.SubActionList != null)
                {
                    SetLevels(question.SubActionList, level + 1);
                }
            }
        }

        private void HandleSelectedBreadcrumb(BreadcrumbEntity breadcrumb)
        {
            if (breadcrumb.Title.Equals("Submit", StringComparison.OrdinalIgnoreCase))
            {
                SummaryQuestions.Clear();
                ShowQuestions = false;
                ShowSummary = true;

                var breadcrumbs = Breadcrumbs.ToList();
                breadcrumbs = breadcrumbs.Where(bc => !bc.Title.Equals("Submit", StringComparison.OrdinalIgnoreCase)).ToList();

                foreach (var bc in breadcrumbs)
                {
                    AddQuestionsToSummaryTab(bc.ActionList);
                }

                LogicPriceCode();
            }
            else
            {
                ShowQuestions = true;
                ShowSummary = false;
                IsLoadingQuestion = true;

                Questions.Clear();
                var actionList = breadcrumb.ActionList.ToList();
                SetLevels(breadcrumb.ActionList);
                AddQuestions(actionList);

                IsLoadingQuestion = false;
            }

        }

        private void AddQuestions(List<ActionItemEntity> questions)
        {
            if (questions == null || !questions.Any())
                return;

            foreach (var question in questions)
            {
                Questions.Add(question);

                RemoveSubList(question);

                var newQuestions = question.SubActionList
                    .Where(a => a.Condition.ResponseData.Equals(question?.Response?.Value, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (newQuestions != null)
                {
                    AddQuestions(newQuestions);
                }
            }
        }

        private void AddQuestionsToSummaryTab(List<ActionItemEntity> questions)
        {
            if (questions == null || !questions.Any())
                return;

            foreach (var question in questions.Where(q => !string.IsNullOrEmpty(q?.Response?.Value)))
            {
                SummaryQuestions.Add(question);

                if (question.SubActionList != null)
                {
                    AddQuestionsToSummaryTab(question.SubActionList);
                }
            }
        }

        private void LogicPriceCode()
        {
            PriceCodes.Clear();
            var actionList = SummaryQuestions.ToList();
            var priceCodeQuestions = actionList.Where(a => !string.IsNullOrEmpty(a.Logic) && a.Logic.Equals(MessageStrings.Logic_Price_Code_777, StringComparison.OrdinalIgnoreCase));

            if (priceCodeQuestions != null && priceCodeQuestions.Any())
            {
                IsShowPriceCodeQTY = true;
                var priceCode777 = new PriceCodeEntity
                {
                    PriceCode = "777"
                };

                var numberOfMeterQuestions = priceCodeQuestions.FirstOrDefault(a => a.Title.Equals(MessageStrings.Number_Of_Meters_Question, StringComparison.OrdinalIgnoreCase));
                if (numberOfMeterQuestions != null && numberOfMeterQuestions.EResponseType == SorEformsResponseType.Number)
                {
                    if (int.TryParse(numberOfMeterQuestions.Response.Value, out int response))
                    {
                        if (response < 5)
                        {
                            priceCode777.QTY = "1";
                        }
                        else if (response >= 5 && response <= 10)
                        {
                            priceCode777.QTY = "3";
                        }
                        else
                        {
                            priceCode777.QTY = "submit for review";
                        }
                    }
                }

                PriceCodes.Add(priceCode777);
            }
        }

        private async Task Cancel()
        {
            await NavigationService.NavigateBackAsync();
        }

        private async Task Confirm()
        {
#if WINDOWS
            await AlertService.ShowAlertAsync(MessageStrings.Submitted_Successfully);
#else
            await UserDialogs.Instance.AlertAsync(MessageStrings.Submitted_Successfully);
#endif
        }
    }
}

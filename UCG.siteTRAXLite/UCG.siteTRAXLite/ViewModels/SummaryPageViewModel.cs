using Acr.UserDialogs;
using System.Windows.Input;
using UCG.siteTRAXLite.Common.Constants;
using UCG.siteTRAXLite.Entities.SorEforms;
using UCG.siteTRAXLite.Managers.Mappers;
using UCG.siteTRAXLite.Managers.SorEformManager;
using UCG.siteTRAXLite.Models;
using UCG.siteTRAXLite.Models.SummaryModels;
using UCG.siteTRAXLite.Services;

namespace UCG.siteTRAXLite.ViewModels
{
    public class SummaryPageViewModel : ViewModelBase
    {
        private int heightPriceCode;
        public int HeightPriceCode
        {
            get { return heightPriceCode; }
            set
            {
                SetProperty(ref heightPriceCode, value);
            }
        }

        public ConcurrentObservableCollection<ActionItemEntity> Actions { get; set; }
        public ConcurrentObservableCollection<PriceCodeEntity> PriceCodes { get; set; }

        private bool showQuestions;
        public bool ShowQuestions
        {
            get { return showQuestions; }
            set
            {
                SetProperty(ref showQuestions, value);
            }
        }

        private string selectedOutcomeOption;
        public string SelectedOutcomeOption
        {
            get { return selectedOutcomeOption; }
            set
            {
                if (value != null)
                {
                    ShowQuestions = true;
                }
                else
                {
                    ShowQuestions = false;
                }
                SetProperty(ref selectedOutcomeOption, value);
            }
        }

        private bool isShowPriceCodeQTY;
        public bool IsShowPriceCodeQTY
        {
            get { return isShowPriceCodeQTY; }
            set
            {
                SetProperty(ref isShowPriceCodeQTY, value);
            }
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

        public SummaryPageViewModel(
            INavigationService navigationService, 
            IAlertService alertService, 
            IOpenAppService openAppService, 
            IServiceEntityMapper mapper) : base(navigationService, alertService, openAppService, mapper)
        {
            Actions = new ConcurrentObservableCollection<ActionItemEntity>();
            PriceCodes = new ConcurrentObservableCollection<PriceCodeEntity>();
            PageTitle = "Jobs";
        }

        public async override Task OnNavigatingTo(object parameter)
        {
            if (parameter != null && parameter is SummaryModel model)
            {
                JobDetail = model.JobDetail;
                foreach (var action in model.Actions)
                {
                    Actions.Add(action);
                }
                SelectedOutcomeOption = model.SelectedOutcomeOption;

                LogicPriceCode();
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

        private void LogicPriceCode()
        {
            var actionList = Actions.ToList();
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
                    if (int.TryParse(numberOfMeterQuestions.Responses, out int response))
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

            HeightPriceCode = PriceCodes.Count() * 50 + 50 > 150 ? 200 : PriceCodes.Count() * 50 + 50;
        }
    }
}

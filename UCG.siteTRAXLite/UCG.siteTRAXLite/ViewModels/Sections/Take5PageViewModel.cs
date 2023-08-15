using Acr.UserDialogs;
using CommunityToolkit.Maui.Views;
using System.Windows.Input;
using UCG.siteTRAXLite.Common.Constants;
using UCG.siteTRAXLite.CustomControls;
using UCG.siteTRAXLite.Entities.SorEforms;
using UCG.siteTRAXLite.Managers.Mappers;
using UCG.siteTRAXLite.Managers.SorEformManager;
using UCG.siteTRAXLite.Models;
using UCG.siteTRAXLite.Models.Take5;
using UCG.siteTRAXLite.Services;

namespace UCG.siteTRAXLite.ViewModels.Sections
{
    public class Take5PageViewModel : ViewModelBase
    {
        private readonly ISorEformManager _sorEformManager;

        public ConcurrentObservableCollection<StepperEntity> Steppers { get; set; }

        private Take5TabModel controlTab;
        public Take5TabModel ControlTab
        {
            get { return controlTab; }
            set
            {
                SetProperty(ref controlTab, value);
            }
        }

        private Take5TabModel hazardTab;
        public Take5TabModel HazardTab
        {
            get { return hazardTab; }
            set
            {
                SetProperty(ref hazardTab, value);
            }
        }

        private Take5TabModel submitTab;
        public Take5TabModel SubmitTab
        {
            get { return submitTab; }
            set
            {
                SetProperty(ref submitTab, value);
            }
        }

        private StepperEntity selectedStepper;
        public StepperEntity SelectedStepper
        {
            get { return selectedStepper; }
            set
            {
                SetProperty(ref selectedStepper, value);
                HandleSelectedStepper(value);
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

        private ICommand showSWMSModalCommand;

        public ICommand ShowSWMSModalCommand
        {
            get
            {
                return this.showSWMSModalCommand ?? (this.showSWMSModalCommand = new Command<ActionItemEntity>(async (q) => await ShowSWMSModal(q)));
            }
        }

        public Take5PageViewModel(
            INavigationService navigationService,
            IAlertService alertService,
            IOpenAppService openAppService,
            IServiceEntityMapper mapper,
            ISorEformManager sorEformManager) : base(navigationService, alertService, openAppService, mapper)
        {
            PageTitle = PageTitles.Take5;

            Steppers = new ConcurrentObservableCollection<StepperEntity>();
            _sorEformManager = sorEformManager;
        }

        public async override Task OnNavigatingTo(object parameter)
        {
            Steppers.Clear();
            await LoadSteppers(parameter as SectionEntity);
        }

        public async Task LoadSteppers(SectionEntity section)
        {
            if (section == null)
                return;

            var take5Steppers = await _sorEformManager.GetTake5Steppers();

            if (take5Steppers != null)
            {
                if (take5Steppers.StepperControl != null)
                {
                    take5Steppers.StepperControl.StepperType = StepperType.Control;
                    ControlTab = new Take5TabModel(take5Steppers.StepperControl);
                    Steppers.Add(take5Steppers.StepperControl);
                }

                if (take5Steppers.StepperHazard != null)
                {
                    take5Steppers.StepperHazard.StepperType = StepperType.Hazard;
                    HazardTab = new Take5TabModel(take5Steppers.StepperHazard);
                    Steppers.Add(take5Steppers.StepperHazard);
                }

                if (take5Steppers.StepperSubmit != null)
                {
                    take5Steppers.StepperSubmit.StepperType = StepperType.Submit;
                    SubmitTab = new Take5TabModel(take5Steppers.StepperSubmit);
                    Steppers.Add(take5Steppers.StepperSubmit);
                }
            }

            SelectedStepper = Steppers.FirstOrDefault();
        }

        private void HandleSelectedStepper(StepperEntity stepper)
        {
            if (stepper == null)
                return;

            ChangeTab(stepper);
        }

        private async void ChangeTab(StepperEntity stepper)
        {
            ControlTab.IsVisible = stepper.StepperType == StepperType.Control;
            HazardTab.IsVisible = stepper.StepperType == StepperType.Hazard;
            SubmitTab.IsVisible = stepper.StepperType == StepperType.Submit;

            if (HazardTab.IsVisible)
                await LoadHazardData(stepper);
        }

        private async Task LoadHazardData(StepperEntity stepper)
        {
            var hazardsDB = await GetHazards();
            foreach (var item in stepper.ActionList)
            {
                var matchedSubAction = item.SubActionList.FirstOrDefault(x =>
                    hazardsDB.Any(h => x.Condition.ResponseData.Equals(h.Name, StringComparison.OrdinalIgnoreCase) && x.EResponseType == SorEformsResponseType.InputTextArea));
                if (matchedSubAction == null)
                    continue;

                var hazard = hazardsDB.FirstOrDefault(h => matchedSubAction.Condition.ResponseData.Equals(h.Name, StringComparison.OrdinalIgnoreCase));

                var selectedData = item.ResponseData
                    .FirstOrDefault(d => matchedSubAction.Condition.ResponseData.Equals(d.Value, StringComparison.OrdinalIgnoreCase));

                selectedData.IsChecked = true;
                matchedSubAction.Response.Value = hazard.Description;
            }
        }

        private void ChangeTab(StepperType type)
        {
            SelectedStepper = Steppers.FirstOrDefault(s => s.StepperType == type);
        }

        public Take5TabModel GetCurrentTab()
        {
            if (SelectedStepper == null)
                return null;

            switch (SelectedStepper.StepperType)
            {
                case StepperType.Control:
                    return ControlTab;
                case StepperType.Hazard:
                    return HazardTab;
                case StepperType.Submit:
                    return SubmitTab;
            }

            return null;
        }

        private async Task ShowSWMSModal(ActionItemEntity question)
        {
            ChangeTab(StepperType.Control);
            var modal = new SWMSModal(question);
            await Application.Current.MainPage.ShowPopupAsync(modal);
        }

        private async Task Cancel()
        {
            await NavigationService.NavigateBackAsync();
        }

        private async Task Confirm()
        {
            await SaveHazard();
#if WINDOWS
                        await AlertService.ShowAlertAsync(MessageStrings.Submitted_Successfully);
#else
            await UserDialogs.Instance.AlertAsync(MessageStrings.Submitted_Successfully);
#endif
        }

        private async Task<bool> SaveHazard()
        {
            await ClearAllHazards();

            var hazards = await GetHazards();

            var checkedAnswers = HazardTab.Questions.Where(x => x.Response.IsChecked).ToList();

            foreach (var answer in checkedAnswers)
            {
                var description = answer.SubActionList.FirstOrDefault(x => x.Condition.ResponseData.Equals(answer.Response.Value, StringComparison.OrdinalIgnoreCase) && x.EResponseType == SorEformsResponseType.InputTextArea);
                var hazard = new HazardEntity()
                {
                    Name = answer.Response.Value,
                    Description = description.Response.Value,
                    SiteAddress = JobDetail.SiteName
                };
                hazards.Add(hazard);
            }

            return await _sorEformManager.SaveListHazard(hazards);
        }

        private async Task<List<HazardEntity>> GetHazards()
        {
            var result = await _sorEformManager.GetHazardsFromLocal();
            return result;
        }

        private async Task<bool> ClearAllHazards()
        {
            return await _sorEformManager.DeleteAllHazards(); ;
        }

    }
}

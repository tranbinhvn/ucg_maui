using Acr.UserDialogs;
using System.Windows.Input;
using UCG.siteTRAXLite.Common.Constants;
using UCG.siteTRAXLite.Entities.SorEforms;
using UCG.siteTRAXLite.Managers.Mappers;
using UCG.siteTRAXLite.Managers.SorEformManager;
using UCG.siteTRAXLite.Models;
using UCG.siteTRAXLite.Models.SorClaims;
using UCG.siteTRAXLite.Services;

namespace UCG.siteTRAXLite.ViewModels.Sections
{
    public class SorClaimsPageViewModel : ViewModelBase
    {
        private readonly ISorEformManager _sorEformManager;

        public ConcurrentObservableCollection<StepperEntity> Steppers { get; set; }

        private ClaimSorsTab sorsTab;
        public ClaimSorsTab SorsTab
        {
            get { return sorsTab; }
            set
            {
                SetProperty(ref sorsTab, value);
            }
        }

        private ClaimUploadFilesTab uploadFilesTab;
        public ClaimUploadFilesTab UploadFilesTab
        {
            get { return uploadFilesTab; }
            set
            {
                SetProperty(ref uploadFilesTab, value);
            }
        }

        private ClaimSubmitTab submitTab;
        public ClaimSubmitTab SubmitTab
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
                HandleSelectedStepper(value);
                SetProperty(ref selectedStepper, value);
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

        public SorClaimsPageViewModel(
            INavigationService navigationService,
            IAlertService alertService,
            IOpenAppService openAppService,
            IServiceEntityMapper mapper,
            ISorEformManager sorEformManager) : base(navigationService, alertService, openAppService, mapper)
        {
            PageTitle = PageTitles.ClaimsPage;

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

            var sorClaimsSteppers = await _sorEformManager.GetSorClaimsSteppers();
            if (sorClaimsSteppers != null)
            {
                if (sorClaimsSteppers.StepperControl != null)
                {
                    sorClaimsSteppers.StepperControl.StepperType = StepperType.Control;
                    SorsTab = new ClaimSorsTab(sorClaimsSteppers.StepperControl);
                    Steppers.Add(sorClaimsSteppers.StepperControl);
                }

                if (sorClaimsSteppers.StepperUploadFiles != null)
                {
                    sorClaimsSteppers.StepperUploadFiles.StepperType = StepperType.UploadFiles;
                    UploadFilesTab = new ClaimUploadFilesTab(sorClaimsSteppers.StepperControl);
                    Steppers.Add(sorClaimsSteppers.StepperUploadFiles);
                }

                if (sorClaimsSteppers.StepperSubmit != null)
                {
                    sorClaimsSteppers.StepperSubmit.StepperType = StepperType.Submit;
                    SubmitTab = new ClaimSubmitTab(sorClaimsSteppers.StepperControl);
                    Steppers.Add(sorClaimsSteppers.StepperSubmit);
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

        private void ChangeTab(StepperEntity stepper)
        {
            SorsTab.IsVisible = stepper.StepperType == StepperType.Control;
            UploadFilesTab.IsVisible = stepper.StepperType == StepperType.UploadFiles;
            SubmitTab.IsVisible = stepper.StepperType == StepperType.Submit;

            if (UploadFilesTab.IsVisible)
            {
                UploadFilesTab.LoadSors(SorsTab.SecondarySOR);
            }
            else if (SubmitTab.IsVisible)
            {

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

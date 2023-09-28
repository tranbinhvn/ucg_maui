using Acr.UserDialogs;
using System.Windows.Input;
using UCG.siteTRAXLite.Common.Constants;
using UCG.siteTRAXLite.DependencyServices;
using UCG.siteTRAXLite.Entities.SorEforms;
using UCG.siteTRAXLite.Managers;
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
        private readonly IUploadManager _uploadManager;
        private readonly IFileService _fileService;
        private readonly IMediaService _mediaService;

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

        private ICommand editPrimaryCommand;

        public ICommand EditPrimaryCommand
        {
            get
            {
                return this.editPrimaryCommand ?? (this.editPrimaryCommand = new Command(() => EditPrimary()));
            }
        }

        private ICommand editSubActionCommand;

        public ICommand EditSubActionCommand
        {
            get
            {
                return this.editSubActionCommand ?? (this.editSubActionCommand = new Command<ActionItemEntity>((q) => EditSubAction(q)));
            }
        }

        public SorClaimsPageViewModel(
            INavigationService navigationService,
            IAlertService alertService,
            IOpenAppService openAppService,
            IServiceEntityMapper mapper,
            ISorEformManager sorEformManager,
            IUploadManager uploadManager,
            IMediaService mediaService,
            IFileService fileService,
            IServiceProvider services) : base(navigationService, alertService, openAppService, mapper, services)
        {
            PageTitle = PageTitles.ClaimsPage;

            Steppers = new ConcurrentObservableCollection<StepperEntity>();
            _sorEformManager = sorEformManager;
            _uploadManager = uploadManager; 
            _mediaService = mediaService;
            _fileService = fileService;
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
                    UploadFilesTab = new ClaimUploadFilesTab(sorClaimsSteppers.StepperControl, AlertService, _uploadManager, _mediaService, _fileService);
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
                SubmitTab.LoadSummaryData(SorsTab.SelectedPrimarySors, SorsTab.SubActions.Where(a => HasValue(a)));
            }
        }

        private void ChangeTab(StepperType type)
        {
            SelectedStepper = Steppers.FirstOrDefault(s => s.StepperType == type);
        }

        private async Task Cancel()
        {
            await NavigationService.NavigateBackAsync();
        }

        private async Task Confirm()
        {
            await AlertService.ShowAlertAsync(MessageStrings.Submitted_Successfully);
        }

        private void EditPrimary()
        {
            ChangeTab(StepperType.Control);
            SorsTab.IsEditUnit = true;
        }

        private void EditSubAction(ActionItemEntity item)
        {
            if (item == null) 
                return;

            ChangeTab(StepperType.Control);
            SorsTab.EditSorCommand.Execute(item);
        }

        public bool HasValue(ActionItemEntity item)
        {
            return item.Response != null
                    && !string.IsNullOrEmpty(item.Response.Value)
                    && !string.IsNullOrEmpty(item.ResponseName);
        }
    }
}

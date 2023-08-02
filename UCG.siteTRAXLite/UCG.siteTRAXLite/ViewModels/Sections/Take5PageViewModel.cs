using Acr.UserDialogs;
using System.Windows.Input;
using UCG.siteTRAXLite.Common.Constants;
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

        public ConcurrentObservableCollection<BreadcrumbEntity> Breadcrumbs { get; set; }

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

        private BreadcrumbEntity selectedBreadcrumb;
        public BreadcrumbEntity SelectedBreadcrumb
        {
            get { return selectedBreadcrumb; }
            set 
            {
                HandleSelectedBreadcrumb(value);
                SetProperty(ref selectedBreadcrumb, value);
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

        public Take5PageViewModel(
            INavigationService navigationService,
            IAlertService alertService,
            IOpenAppService openAppService,
            IServiceEntityMapper mapper,
            ISorEformManager sorEformManager) : base(navigationService, alertService, openAppService, mapper)
        {
            PageTitle = "TAKE 5";

            Breadcrumbs = new ConcurrentObservableCollection<BreadcrumbEntity>();
            _sorEformManager = sorEformManager;
        }

        public async override Task OnNavigatingTo(object parameter)
        {
            Breadcrumbs.Clear();
            await LoadBreadcrumbs(parameter as SectionEntity);
        }

        public async Task LoadBreadcrumbs(SectionEntity section)
        {
            if (section != null)
            {
                var take5Breadcrumbs = await _sorEformManager.GetTake5Breadcrumbs();

                if (take5Breadcrumbs != null)
                {
                    if (take5Breadcrumbs.BreadcrumbControl != null)
                    {
                        take5Breadcrumbs.BreadcrumbControl.BreadcrumbType = BreadcrumbType.Take5Control;
                        ControlTab = new Take5TabModel(take5Breadcrumbs.BreadcrumbControl);
                        Breadcrumbs.Add(take5Breadcrumbs.BreadcrumbControl);
                    }

                    if (take5Breadcrumbs.BreadcrumbHazard != null)
                    {
                        take5Breadcrumbs.BreadcrumbHazard.BreadcrumbType = BreadcrumbType.Take5Hazard;
                        HazardTab = new Take5TabModel(take5Breadcrumbs.BreadcrumbHazard);
                        Breadcrumbs.Add(take5Breadcrumbs.BreadcrumbHazard);
                    }

                    if (take5Breadcrumbs.BreadcrumbSubmit != null)
                    {
                        take5Breadcrumbs.BreadcrumbSubmit.BreadcrumbType = BreadcrumbType.Take5Submit;

                        SubmitTab = new Take5TabModel(take5Breadcrumbs.BreadcrumbSubmit);

                        Breadcrumbs.Add(take5Breadcrumbs.BreadcrumbSubmit);
                    }
                }

                SelectedBreadcrumb = Breadcrumbs.FirstOrDefault();
            }
        }

        private void HandleSelectedBreadcrumb(BreadcrumbEntity breadcrumb)
        {
            if (breadcrumb == null)
                return;

            ChangeTab(breadcrumb);
        }

        private void ChangeTab(BreadcrumbEntity breadcrumb)
        {
            ControlTab.IsVisible = breadcrumb.BreadcrumbType == BreadcrumbType.Take5Control;
            HazardTab.IsVisible = breadcrumb.BreadcrumbType == BreadcrumbType.Take5Hazard;
            SubmitTab.IsVisible = breadcrumb.BreadcrumbType == BreadcrumbType.Take5Submit;
        }

        public Take5TabModel GetCurrentTab()
        {
            if (SelectedBreadcrumb?.BreadcrumbType == BreadcrumbType.Take5Control)
                return ControlTab;
            else if (SelectedBreadcrumb?.BreadcrumbType == BreadcrumbType.Take5Hazard)
                return HazardTab;
            else if (SelectedBreadcrumb?.BreadcrumbType == BreadcrumbType.Take5Submit)
                return SubmitTab;

            return null;
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

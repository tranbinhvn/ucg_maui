using Acr.UserDialogs;
using CommunityToolkit.Mvvm.Messaging;
using Newtonsoft.Json;
using System.Windows.Input;
using UCG.siteTRAXLite.Common.Constants;
using UCG.siteTRAXLite.DataContracts;
using UCG.siteTRAXLite.Entities;
using UCG.siteTRAXLite.Entities.Job;
using UCG.siteTRAXLite.Entities.Site;
using UCG.siteTRAXLite.Extensions;
using UCG.siteTRAXLite.Managers.Mappers;
using UCG.siteTRAXLite.Managers.SiteAndJob;
using UCG.siteTRAXLite.Messages;
using UCG.siteTRAXLite.Models;
using UCG.siteTRAXLite.Services;
using UCG.siteTRAXLite.Utils;
using UCG.siteTRAXLite.WebServices.Exceptions;

namespace UCG.siteTRAXLite.ViewModels
{
    public class ViewModelBase : BindableBase
    {
        protected INavigationService NavigationService { get; private set; }
        protected IAlertService AlertService { get; private set; }
        protected IOpenAppService OpenAppService { get; private set; }
        protected IServiceEntityMapper Mapper { get; private set; }
        protected IServiceProvider Services { get; private set; }

        public static bool _isNetworkConnected;
        public bool IsNetworkConnected
        {
            get { return _isNetworkConnected; }
            set { SetProperty(ref _isNetworkConnected, value); OnPropertyChanged(nameof(NetworkDisconnected)); }
        }

        private bool isCommandExecuting;
        public bool IsCommandExecuting
        {
            get { return isCommandExecuting; }
            set { SetProperty(ref isCommandExecuting, value); }
        }

        public bool NetworkDisconnected
        {
            get { return !_isNetworkConnected; }
        }

        private string _pageTitle;
        public string PageTitle
        {
            get { return _pageTitle; }
            set { SetProperty(ref _pageTitle, value); }
        }

        private JobEntity jobDetail;
        public JobEntity JobDetail
        {
            get
            {
                return jobDetail;
            }
            set
            {
                SetProperty(ref jobDetail, value);
            }
        }

        private ICommand goBackCommand;

        public ICommand GoBackCommand
        {
            get
            {
                return this.goBackCommand ?? (this.goBackCommand = new Command(async () => await GoBack()));
            }
        }

        public ViewModelBase(
            INavigationService navigationService, 
            IAlertService alertService,
            IOpenAppService openAppService,
            IServiceEntityMapper mapper,
            IServiceProvider services)
        {
            NavigationService = navigationService;
            Mapper = mapper;

            var accessType = Connectivity.Current.NetworkAccess;

            IsNetworkConnected = accessType == NetworkAccess.Internet;
            AlertService = alertService;
            OpenAppService = openAppService;
            Services = services;

            var jobManager = Services.GetService<IJobManager>();

            var job = jobManager.LoadJobInfo();
            if (job == null)
            {
                var jobK = Guid.NewGuid();
                var siteK = Guid.NewGuid();
                JobDetail = new JobEntity()
                {
                    JobK = jobK,
                    SiteFK = siteK,
                    Site = new SiteEntity()
                    {
                        SiteK = siteK,
                        SiteName = "7 FINLAYSON BROOK ROAD Waipu 0582",
                        CRN = "221226808423",
                    },
                    JobType = "CHS-PRO-Install",
                    WorkflowStatus = "DP Arrived",
                    Age = "1 Day",
                    PlannedStartDate = DateTime.Now,
                    PlannedEndDate = DateTime.Now
                };

                jobManager.SaveSiteAndJob(JobDetail);
            }
            else
            {
                JobDetail = job;
            }

            WeakReferenceMessenger.Default.Unregister<LaunchingAppMessage>(this);
            WeakReferenceMessenger.Default.Register<LaunchingAppMessage>(this, (r, data) =>
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    if (!string.IsNullOrEmpty(data.Value))
                    {
                        var launchDataDto = JsonConvert.DeserializeObject<LaunchDataDTO>(data.Value);
                        var launchDataEntity = Mapper.Map<LaunchDataEntity>(launchDataDto);

                        if (launchDataEntity != null)
                        {
                            JobDetail.Site.CRN = launchDataEntity?.JobDetail?.CRN;
                            JobDetail.Site.SiteName = launchDataEntity?.JobDetail?.SiteName;
                        }
                    }
                });
            });
        }

        public virtual Task OnNavigatingTo(object parameter)
            => Task.CompletedTask;

        public virtual Task OnNavigatedFrom(bool isForwardNavigation)
            => Task.CompletedTask;

        public virtual Task OnNavigatedTo()
            => Task.CompletedTask;

        public void HandleNetworkException(NetworkException e)
        {
            if (!IsNetworkConnected)
                return;

            // Custom Alert if api not found
            if (e.ResponseCode == ResponseCode.APINOTFOUND)
            {
#if WINDOWS
                AlertService.ShowAlert(MessageStrings.APINotFoundContent, MessageStrings.APINotFoundTitle);
#else
                UserDialogs.Instance.Alert(MessageStrings.APINotFoundContent, MessageStrings.APINotFoundTitle);
#endif
                return;
            }
            var errorMsg = ExceptionHandler.GetErrorMessage(e.ResponseCode, e.Message);
            if (!string.IsNullOrEmpty(errorMsg) || !string.IsNullOrWhiteSpace(errorMsg))
            {
#if WINDOWS
                AlertService.ShowAlert(errorMsg);
#else
                UserDialogs.Instance.Alert(errorMsg);
#endif
            }
        }

        public async Task GoBack()
        {
            await NavigationService.NavigateBackAsync();
        }
    }
}

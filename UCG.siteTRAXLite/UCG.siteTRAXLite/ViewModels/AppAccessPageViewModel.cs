using Acr.UserDialogs;
using System.Windows.Input;
using UCG.siteTRAXLite.Common.Constants;
using UCG.siteTRAXLite.Extensions;
using UCG.siteTRAXLite.Managers;
using UCG.siteTRAXLite.Managers.UserDatas;
using UCG.siteTRAXLite.Services;

namespace UCG.siteTRAXLite.ViewModels
{
    public class AppAccessPageViewModel : ViewModelBase
    {
        private readonly IUserData _userData;
        private readonly IOpenAppService _openAppService;

        private ICommand accessAppCommand;
        public ICommand AccessAppCommand
        {
            get
            {
                return this.accessAppCommand ?? (this.accessAppCommand = new Command(() => this.AccessApp()));
            }
        }

        public AppAccessPageViewModel(INavigationService navigationService, 
            IUserData userData, 
            IOpenAppService openAppService,
            IAlertService alertService) : base(navigationService, alertService)
        {
            _userData = userData;
            _openAppService = openAppService;
        }

        public async void AccessApp()
        {
            try
            {
                var isSuccess = false;

                var text = "Data from SiteTRAX Lite";

#if ANDROID
                isSuccess = await FuncEx.ExcuteAsync(_openAppService.LaunchApp, MessageStrings.SiteTraxAir_Package_Name, text);
#elif IOS
                isSuccess = await FuncEx.ExcuteAsync(_openAppService.LaunchApp, MessageStrings.SiteTraxAir_Uri, text);
#endif

                if (!isSuccess)
                {
#if WINDOWS
                    await AlertService.ShowAlertAsync(MessageStrings.Not_Installed_App);
#else
                    await UserDialogs.Instance.AlertAsync(MessageStrings.Not_Installed_App);
#endif
                }
            } catch(Exception ex)
            {
#if WINDOWS
                await AlertService.ShowAlertAsync(ex.Message);
#else
                await UserDialogs.Instance.AlertAsync(ex.Message);
#endif
            }
        }
    }
}

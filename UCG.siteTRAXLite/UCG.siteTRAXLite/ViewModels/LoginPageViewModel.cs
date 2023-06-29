using Acr.UserDialogs;
using System.Windows.Input;
using UCG.siteTRAXLite.Common.Constants;
using UCG.siteTRAXLite.DataContracts;
using UCG.siteTRAXLite.Managers.Mappers;
using UCG.siteTRAXLite.Managers.UserDatas;
using UCG.siteTRAXLite.Managers.UserManagers;
using UCG.siteTRAXLite.Services;
using UCG.siteTRAXLite.Utils;
using UCG.siteTRAXLite.WebServices.AuthenticationServices;
using UCG.siteTRAXLite.WebServices.Exceptions;
using UCG.siteTRAXLite.WebServices.Helper;

namespace UCG.siteTRAXLite.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly IServiceEntityMapper Mapper;
        private readonly IIdentityService _authenService;
        private readonly IUserManager _userManager;
        private readonly IUserData _userData;

        private string _username;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        private bool _alreadyLogin;
        public bool AlreadyLogin
        {
            get { return _alreadyLogin; }
            set { SetProperty(ref _alreadyLogin, value); }
        }

        private bool _rememberMe;
        public bool RememberMe
        {
            get { return _rememberMe; }
            set { SetProperty(ref _rememberMe, value); }
        }

        private ICommand loginCommand;
        public ICommand LoginCommand
        {
            get
            {
                return this.loginCommand ?? (this.loginCommand = new Command(() => this.Login()));
            }
        }

        private ICommand settingCommand;


        public ICommand SettingCommand
        {
            get
            {
                return this.settingCommand ?? (this.settingCommand = new Command(async () => await Setting()));
            }
        }

        private ICommand rememberCommand;
        public ICommand RememberCommand
        {
            get
            {
                return this.rememberCommand ?? (this.rememberCommand = new Command(RememberMeChange));
            }
        }

        public LoginPageViewModel(INavigationService navigationService, 
            IIdentityService authenService, 
            IUserManager userManager,
            IUserData userData, 
            IAlertService alertService) : base(navigationService, alertService)
        {
            _authenService = authenService;
            _userManager = userManager;
            _userData = userData;

            RememberMe = Settings.IsRememberMe;
            if (RememberMe)
            {
                Username = Settings.UserNameSetting;
                Password = Settings.PasswordSetting;
                Settings.Country = Settings.SelectedCountry;
            }
            else
            {
                Settings.Country = string.Empty;
                Settings.SelectedCountry = string.Empty;
                Settings.EndPointSettingKeyString = string.Empty;
            }
        }

        private async void Login()
        {
#if WINDOWS
            var spinner = await SpinnerHelper.ShowSpinnerAsync(MessageStrings.Login);
#else
            MainThread.BeginInvokeOnMainThread(() => UserDialogs.Instance.ShowLoading(MessageStrings.Login, MaskType.Black));
#endif

            await Task.Run(async () =>
            {
                if (string.IsNullOrEmpty(Settings.SelectedCountry))
                {
#if WINDOWS
                    await AlertService.ShowAlertAsync(MessageStrings.NoEndpointSelected);
#else
                    await UserDialogs.Instance.AlertAsync(MessageStrings.NoEndpointSelected).ConfigureAwait(true);
#endif
                    return;
                }

                if (!RememberMe)
                {
                    Settings.Logout();
                }
                var isSuccess = await _authenService.Login(Username, Password);
                bool shouldUpdateCompany = false;
                if (isSuccess.Code == ResponseCode.SUCCESS)
                {
                    if (RememberMe)
                    {
                        shouldUpdateCompany = (!Username.Equals(Settings.UserNameSetting))
                            || string.IsNullOrEmpty(Settings.CompanyKeySetting);
                        Settings.IsRememberMe = RememberMe;
                        Settings.UserNameSetting = Username;
                        Settings.PasswordSetting = Password;
                    }
                    else
                    {
                        shouldUpdateCompany = true;
                    }

                    await SetupUserInfo(shouldUpdateCompany, true);

                    MainThread.BeginInvokeOnMainThread(async () => 
                    {
                        await NavigationService.NavigateToAsync(PageNames.AppAccessPage);
                    });
                }
                else
                {
#if WINDOWS
                    await AlertService.ShowAlertAsync(isSuccess.ErrorDescription);
#else
                    await UserDialogs.Instance.AlertAsync(isSuccess.ErrorDescription).ConfigureAwait(true);
#endif
                }

            }).ContinueWith(res => MainThread.BeginInvokeOnMainThread(async () =>
            {
#if WINDOWS
                await SpinnerHelper.CloseSpinnerAsync(spinner);
#else
                UserDialogs.Instance.HideLoading();
#endif
            }));
        }

        private async Task SetupUserInfo(bool shouldUpdateCompany, bool isOnline)
        {
            try
            {
                var userInfoResponse = await _userManager.GetUserInfo(IsNetworkConnected);
                var userInfo = userInfoResponse;
                _userData.SetUserInfo(userInfo);
            }
            catch (NetworkException ex)
            {
                HandleNetworkException(ex);
            }
        }

        private void RememberMeChange()
        {
            if (!RememberMe)
            {
                Settings.IsRememberMe = false;
                Settings.UserNameSetting = string.Empty;
                Settings.PasswordSetting = string.Empty;
            }
        }

        private async Task Setting()
        {
            await NavigationService.NavigateToAsync(PageNames.SettingsPage);
        }
    }
}

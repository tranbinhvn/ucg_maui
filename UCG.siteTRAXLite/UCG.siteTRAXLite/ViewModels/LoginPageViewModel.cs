using Acr.UserDialogs;
using System.Windows.Input;
using UCG.siteTRAXLite.Constants;
using UCG.siteTRAXLite.DataContracts;
using UCG.siteTRAXLite.Managers;
using UCG.siteTRAXLite.Mappers;
using UCG.siteTRAXLite.Services;
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

        public LoginPageViewModel(INavigationService navigationService, IIdentityService authenService, IUserManager userManager) : base(navigationService)
        {
            _authenService = authenService;
            _userManager = userManager;

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

        private void Login()
        {
#if ANDROID
            MainThread.BeginInvokeOnMainThread(() => UserDialogs.Instance.ShowLoading(MessageStrings.Login, MaskType.Black));
#endif

            Task.Run(async () =>
            {
                if (string.IsNullOrEmpty(Settings.SelectedCountry))
                {
#if ANDROID
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
                }
                else
                {
#if ANDROID 
                    await UserDialogs.Instance.AlertAsync(isSuccess.ErrorDescription).ConfigureAwait(true);
#endif
                }

            }).ContinueWith(res => MainThread.BeginInvokeOnMainThread(() =>
            {
#if ANDROID
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

#if ANDROID
                await UserDialogs.Instance.AlertAsync("Login Successfully!").ConfigureAwait(true);
#endif
            }
            catch (NetworkException ex)
            {
                HandleNetworkException(ex);
            }
        }

        private async Task Setting()
        {
            await NavigationService.NavigateToAsync(PageNames.SettingsPage);
        }
    }
}

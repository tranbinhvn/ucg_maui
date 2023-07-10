using Acr.UserDialogs;
using CommunityToolkit.Mvvm.Messaging;
using UCG.siteTRAXLite.Common.Constants;
using UCG.siteTRAXLite.DataContracts;
using UCG.siteTRAXLite.Messages;
using UCG.siteTRAXLite.Services;
using UCG.siteTRAXLite.Utils;
using UCG.siteTRAXLite.WebServices.Exceptions;

namespace UCG.siteTRAXLite.ViewModels
{
    public class ViewModelBase : BindableBase
    {
        protected INavigationService NavigationService { get; private set; }
        protected IAlertService AlertService { get; private set; }

        public static bool _isNetworkConnected;
        public bool IsNetworkConnected
        {
            get { return _isNetworkConnected; }
            set { SetProperty(ref _isNetworkConnected, value); OnPropertyChanged(nameof(NetworkDisconnected)); }
        }

        public bool NetworkDisconnected
        {
            get { return !_isNetworkConnected; }
        }

        public ViewModelBase(INavigationService navigationService, IAlertService alertService)
        {
            NavigationService = navigationService;

            var accessType = Connectivity.Current.NetworkAccess;

            IsNetworkConnected = accessType == NetworkAccess.Internet;
            AlertService = alertService;
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
    }
}

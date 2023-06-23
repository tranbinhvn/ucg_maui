using Acr.UserDialogs;
using UCG.siteTRAXLite.Constants;
using UCG.siteTRAXLite.DataContracts;
using UCG.siteTRAXLite.Services;
using UCG.siteTRAXLite.Utils;
using UCG.siteTRAXLite.WebServices.Exceptions;

namespace UCG.siteTRAXLite.ViewModels
{
    public class ViewModelBase : BindableBase
    {
        protected INavigationService NavigationService { get; private set; }

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

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;

            var accessType = Connectivity.Current.NetworkAccess;

            IsNetworkConnected = accessType == NetworkAccess.Internet;
        }

        public void HandleNetworkException(NetworkException e)
        {
            if (!IsNetworkConnected)
                return;

            // Custom Alert if api not found
            if (e.ResponseCode == ResponseCode.APINOTFOUND)
            {
#if ANDROID
                UserDialogs.Instance.Alert(MessageStrings.APINotFoundContent, MessageStrings.APINotFoundTitle);
#endif
                return;
            }
            var errorMsg = ExceptionHandler.GetErrorMessage(e.ResponseCode, e.Message);
            if (!string.IsNullOrEmpty(errorMsg) || !string.IsNullOrWhiteSpace(errorMsg))
            {
#if ANDROID
                UserDialogs.Instance.Alert(errorMsg);
#endif
            }
        }
    }
}

using Acr.UserDialogs;

namespace UCG.siteTRAXLite.Services
{
    public class AlertService : IAlertService
    {
        private Page MainPage
        {
            get
            {
                return Application.Current.MainPage;
            }
        }

        public void ShowAlert(string message, string title = "", string cancel = "OK")
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
#if WINDOWS
                MainPage.DisplayAlert(title, message, cancel);
#else
                UserDialogs.Instance.Alert(message, title, cancel);
#endif
            });
        }

        public async Task ShowAlertAsync(string message, string title = "", string cancel = "OK")
        {
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
#if WINDOWS
                MainPage.DisplayAlert(title, message, cancel);
#else
                UserDialogs.Instance.Alert(message, title, cancel);
#endif
            });
        }
    }
}

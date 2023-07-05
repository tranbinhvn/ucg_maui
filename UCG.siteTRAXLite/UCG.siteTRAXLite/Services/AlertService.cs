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
                MainPage.DisplayAlert(title, message, cancel);
            });
        }

        public async Task ShowAlertAsync(string message, string title = "", string cancel = "OK")
        {
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                MainPage.DisplayAlert(title, message, cancel);
            });
        }
    }
}

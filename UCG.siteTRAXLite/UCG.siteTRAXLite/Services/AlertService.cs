namespace UCG.siteTRAXLite.Services
{
    public class AlertService : IAlertService
    {
        public void ShowAlert(string message, string title = "", string cancel = "OK")
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Shell.Current.DisplayAlert(title, message, cancel);
            });
        }

        public async Task ShowAlertAsync(string message, string title = "", string cancel = "OK")
        {
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                Shell.Current.DisplayAlert(title, message, cancel);
            });
        }
    }
}

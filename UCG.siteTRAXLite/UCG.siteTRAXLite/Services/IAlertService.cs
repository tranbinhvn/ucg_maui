namespace UCG.siteTRAXLite.Services
{
    public interface IAlertService
    {
        Task ShowAlertAsync(string message, string title = "", string cancel = "OK");
        void ShowAlert(string message, string title = "", string cancel = "OK");
    }
}

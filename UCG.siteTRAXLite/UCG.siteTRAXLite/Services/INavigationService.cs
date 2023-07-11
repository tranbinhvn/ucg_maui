namespace UCG.siteTRAXLite.Services
{
    public interface INavigationService
    {
        Task NavigateToPageAsync<T>(object parameter = null) where T : Page;

        Task NavigateBackAsync();
    }
}

namespace UCG.siteTRAXLite.Services
{
    public interface INavigationService
    {
        Task NavigateToAsync(string route);

        Task NavigateToAsync(string route, IDictionary<string, object> routeParameters);

        Task PopAsync();
        Task PopAsync(IDictionary<string, object> routeParameters);
    }
}

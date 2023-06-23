namespace UCG.siteTRAXLite.Services
{
    public class NavigationService : INavigationService
    {
        public async Task NavigateToAsync(string route)
        {
            
            await Shell.Current.GoToAsync(route);
        }

        public async Task NavigateToAsync(string route, IDictionary<string, object> routeParameters)
        {
            await Shell.Current.GoToAsync(route, routeParameters);
        }

        public async Task PopAsync()
        {
            await Shell.Current.GoToAsync("..");
        }

        public async Task PopAsync(IDictionary<string, object> routeParameters)
        {
            await Shell.Current.GoToAsync("..", routeParameters);
        }
    }
}

using UCG.siteTRAXLite.ViewModels;

namespace UCG.siteTRAXLite.Services
{
    public class NavigationService : INavigationService
    {
        readonly IServiceProvider _services;

        protected INavigation Navigation
        {
            get
            {
                return Application.Current?.MainPage?.Navigation;
            }
        }

        public NavigationService(IServiceProvider services)
        {
            _services = services;
        }

        public async Task NavigateToPageAsync<T>(object parameter = null) where T : Page
        {
            var toPage = ResolvePage<T>();
            if (toPage != null)
            {
                toPage.NavigatedTo += Page_NavigatedTo;

                var toViewModel = GetPageViewModelBase(toPage);
                if (toViewModel != null)
                    await toViewModel.OnNavigatingTo(parameter);

                await Navigation.PushAsync(toPage, true);

                toPage.NavigatedFrom += Page_NavigatedFrom;
            }
        }

        public async Task NavigateBackAsync()
        {
            if (Navigation.NavigationStack.Count > 1)
                await Navigation.PopAsync();
        }

        private async void Page_NavigatedTo(object sender, NavigatedToEventArgs e)
        {
            var page = sender as Page;

            var fromViewModel = GetPageViewModelBase(page);
            if (fromViewModel != null)
                await fromViewModel.OnNavigatedTo();
        }


        private async void Page_NavigatedFrom(object sender, NavigatedFromEventArgs e)
        {
            bool isForwardNavigation = Navigation.NavigationStack.Count > 1
                && Navigation.NavigationStack[^2] == sender;
            if (sender is Page thisPage)
            {
                if (!isForwardNavigation)
                {
                    thisPage.NavigatedTo -= Page_NavigatedTo;
                    thisPage.NavigatedFrom -= Page_NavigatedFrom;
                }

                var fromViewModel = GetPageViewModelBase(thisPage);
                if (fromViewModel is not null)
                    await fromViewModel.OnNavigatedFrom(isForwardNavigation);
            }
        }

        private T ResolvePage<T>() where T : Page
            => _services.GetService<T>();

        private ViewModelBase GetPageViewModelBase(Page p)
            => p?.BindingContext as ViewModelBase;
    }
}

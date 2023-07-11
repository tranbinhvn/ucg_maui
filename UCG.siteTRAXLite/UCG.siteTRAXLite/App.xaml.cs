using UCG.siteTRAXLite.Services;
using UCG.siteTRAXLite.Views;

namespace UCG.siteTRAXLite
{
    public partial class App : Application
    {
        public App(INavigationService navigationService)
        {
            InitializeComponent();
            MainPage = new NavigationPage();
            navigationService.NavigateToPageAsync<SorEformPage>();
        }
    }
}
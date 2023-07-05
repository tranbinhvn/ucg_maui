using UCG.siteTRAXLite.Views;

namespace UCG.siteTRAXLite
{
    public partial class App : Application
    {
        public App(SorEformPage sorEformPage)
        {
            InitializeComponent();

            MainPage = new NavigationPage(sorEformPage);
        }
    }
}
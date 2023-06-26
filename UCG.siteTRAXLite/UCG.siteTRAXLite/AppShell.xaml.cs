using UCG.siteTRAXLite.Constants;
using UCG.siteTRAXLite.Views;

namespace UCG.siteTRAXLite
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(PageNames.LoginPage, typeof(LoginPage));
            Routing.RegisterRoute(PageNames.SettingsPage, typeof(SettingsPage));
            Routing.RegisterRoute(PageNames.AppAccessPage, typeof(AppAccessPage));
        }
    }
}
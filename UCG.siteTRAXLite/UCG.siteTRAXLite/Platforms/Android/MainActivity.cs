using Android.App;
using Android.Content.PM;
using Android.OS;
using CommunityToolkit.Mvvm.Messaging;
using UCG.siteTRAXLite.Messages;

namespace UCG.siteTRAXLite
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            GetDataFromSiteTraxAir();
        }

        private void GetDataFromSiteTraxAir()
        {
            if (Intent != null && Intent.HasExtra("data"))
            {
                var data = Intent.GetStringExtra("data");
                WeakReferenceMessenger.Default.Send(new LaunchingAppMessage(data));
            }
        }
    }
}
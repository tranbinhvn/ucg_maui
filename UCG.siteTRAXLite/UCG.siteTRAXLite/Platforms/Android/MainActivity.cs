using Android.App;
using Android.Content;
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

            GetDataFromSiteTraxAir(Intent);
        }

        private void GetDataFromSiteTraxAir(Intent intent)
        {
            if (intent != null && intent.HasExtra("data"))
            {
                var data = intent.GetStringExtra("data");
                WeakReferenceMessenger.Default.Send(new LaunchingAppMessage(data));
            }
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            GetDataFromSiteTraxAir(intent);
        }
    }
}
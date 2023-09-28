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
        internal const string READ_MEDIA_IMAGES = "android.permission.READ_MEDIA_IMAGES";
        internal const string READ_MEDIA_VIDEO = "android.permission.READ_MEDIA_VIDEO";
        internal const string READ_MEDIA_AUDIO = "android.permission.READ_MEDIA_AUDIO";

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
            RequestPermissionsOnStart();
        }

        void RequestPermissionsOnStart()
        {
            if ((int)Build.VERSION.SdkInt < 33) return;
            string[] Permissions =
            {
                   READ_MEDIA_IMAGES,
                   READ_MEDIA_VIDEO,
                   READ_MEDIA_AUDIO,
            };
            int requestId = 0;
            RequestPermissions(Permissions, requestId);
        }
    }
}
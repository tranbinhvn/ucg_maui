#if ANDROID
using Android.Content.PM;
using Android.Content;
#endif

namespace UCG.siteTRAXLite.Services
{
    public class OpenAppService : IOpenAppService
    {
#if ANDROID
        public bool IsAppInstalled(string packageName)
        {
            var installed = false;
            var pm = Android.App.Application.Context.PackageManager;
            try
            {
                pm.GetPackageInfo(packageName, PackageInfoFlags.Activities);
                installed = true;
            }
            catch (PackageManager.NameNotFoundException e)
            {
                installed = false;
            }
            return installed;
        }
#endif

        public Task<bool> LaunchApp(string packageName, string data = null)
        {
            bool result = false;
#if ANDROID
            try
            {
                var pm = Android.App.Application.Context.PackageManager;

                if (IsAppInstalled(packageName))
                {
                    var intent = pm.GetLaunchIntentForPackage(packageName);
                    if (intent != null)
                    {
                        intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.SingleTop);
                        if (!string.IsNullOrEmpty(data))
                        {
                            intent.PutExtra("data", data);
                        }
                        Android.App.Application.Context.StartActivity(intent);
                        result = true;
                    }
                }
            }
            catch (ActivityNotFoundException)
            {
                result = false;
            }
#elif IOS
            return Launcher.Default.TryOpenAsync(packageName + $"?data={data}");
#endif
            return Task.FromResult(result);
        }
    }
}

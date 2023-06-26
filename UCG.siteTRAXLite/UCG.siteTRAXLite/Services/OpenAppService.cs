#if ANDROID
using Android.Content.PM;
using Android.Content;
#endif

namespace UCG.siteTRAXLite.Services
{
    public class OpenAppService : IOpenAppService
    {
        public bool IsAppInstalled(string packageName)
        {
            var installed = false;
#if ANDROID
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
#endif
            return installed;
        }

        public Task<bool> LaunchApp(string packageName)
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
                        intent.SetFlags(ActivityFlags.NewTask);
                        Android.App.Application.Context.StartActivity(intent);
                        result = true;
                    }
                }
            }
            catch (ActivityNotFoundException)
            {
                result = false;
            }
#endif
            return Task.FromResult(result);
        }
    }
}

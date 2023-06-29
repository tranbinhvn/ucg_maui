#if ANDROID
using Android.Content.PM;
using Android.Content;
#endif

#if IOS
using Foundation;
using UIKit;
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
#elif IOS
            try
            {
                var canOpen = UIApplication.SharedApplication.CanOpenUrl(new NSUrl(packageName));
                if (!canOpen)
                    return Task.FromResult(false);
                return Task.FromResult(UIApplication.SharedApplication.OpenUrl(new NSUrl(packageName)));
            }
            catch (Exception ex)
            {
                return Task.FromResult(false);
            }
#endif
            return Task.FromResult(result);
        }
    }
}

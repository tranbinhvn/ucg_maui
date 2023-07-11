using CommunityToolkit.Mvvm.Messaging;
using Foundation;
using UCG.siteTRAXLite.Messages;
using UIKit;

namespace UCG.siteTRAXLite
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        public override bool OpenUrl(UIApplication application, NSUrl url, NSDictionary options)
        {
            var urlComponents = new NSUrlComponents(url, false);
            var queryItems = urlComponents?.QueryItems;
            var data = queryItems?.FirstOrDefault(item => item.Name == "data")?.Value;

            WeakReferenceMessenger.Default.Send(new LaunchingAppMessage(data));

            return true;
        }
    }
}
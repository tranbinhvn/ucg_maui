#if ANDROID
using Javax.Net.Ssl;
using Xamarin.Android.Net;
#endif

namespace UCG.siteTRAXLite.Helpers
{
    public class HttpClientHelper
    {
        public static HttpClient GetInsecureHttpClient()
        {
#if ANDROID
            var handler = new CustomAndroidMessageHandler();
#else
            var handler = new HttpClientHandler();
#endif
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            HttpClient client = new HttpClient(handler);
            return client;
        }
    }

#if ANDROID
    public sealed class CustomAndroidMessageHandler : AndroidMessageHandler
    {
        protected override IHostnameVerifier GetSSLHostnameVerifier(HttpsURLConnection connection)
            => new CustomHostnameVerifier();

        private sealed class CustomHostnameVerifier : Java.Lang.Object, IHostnameVerifier
        {
            public bool Verify(string hostname, ISSLSession session)
                => HttpsURLConnection.DefaultHostnameVerifier.Verify(hostname, session)
                   || (hostname == "10.0.2.2" && session.PeerPrincipal.Name == "CN=localhost");
        }
    }
#endif
}

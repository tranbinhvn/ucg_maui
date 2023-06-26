using CommunityToolkit.Maui.Views;
using UCG.siteTRAXLite.CustomControls;

namespace UCG.siteTRAXLite.Utils
{
    public static class SpinnerHelper
    {
#if WINDOWS
        public async static Task<Popup> ShowSpinnerAsync(string message)
        {
            Popup popup = null;

            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                var spinnerView = new CustomSpinner(message);
                popup = new Popup
                {
                    Content = spinnerView,
                    Color = Colors.Transparent,
                    CanBeDismissedByTappingOutsideOfPopup = false
                };

                Shell.Current.ShowPopup(popup);
            });

            return popup;
        }

        public static async Task CloseSpinnerAsync(Popup popup)
        {
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                popup.Close();
            });
        }
#endif
    }
}

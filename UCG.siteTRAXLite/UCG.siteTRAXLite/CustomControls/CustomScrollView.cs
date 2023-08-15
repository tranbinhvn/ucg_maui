using Microsoft.Maui.Handlers;
#if IOS
using UIKit;
#endif

namespace UCG.siteTRAXLite.CustomControls
{
    public class CustomScrollView : ScrollView
    {
        public CustomScrollView()
        {
#if IOS
            ScrollViewHandler.Mapper.AppendToMapping(nameof(IScrollView.ContentSize), OnScrollViewContentSizePropertyChanged);
#endif
        }

#if IOS
        private void OnScrollViewContentSizePropertyChanged(IScrollViewHandler _, IScrollView __)
        {
            if (Handler?.PlatformView is not UIView platformUiView)
                return;
      
            if (platformUiView.Subviews.FirstOrDefault() is not { } contentView)
                return;
      
            contentView.SizeToFit();
        }
#endif
    }
}

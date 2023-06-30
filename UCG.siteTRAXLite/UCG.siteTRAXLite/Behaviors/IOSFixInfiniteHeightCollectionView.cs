namespace UCG.siteTRAXLite.Behaviors
{
    public class IOSFixInfiniteHeightCollectionView : Behavior<CollectionView>
    {
        private void CollectionViewOnSizeChanged(object? sender, EventArgs e)
        {
#if IOS
            if (sender is not CollectionView collectionView)
            {
                throw new InvalidOperationException();
            }
            if (collectionView.Handler?.PlatformView is not UIKit.UIView uiView)
            {
                throw new InvalidOperationException();
            }

            var uiCollectionView = uiView.Subviews
                .OfType<UIKit.UICollectionView>()
                .Single();

            const double emptyViewRequiredHeight = 50;
            const double additionalHeight = 20; 
            var calculatedHeight = (double)uiCollectionView.CollectionViewLayout.CollectionViewContentSize.Height;
            var height = additionalHeight + calculatedHeight;
            collectionView.MaximumHeightRequest = Math.Max(emptyViewRequiredHeight, height);
#endif
        }

        protected override void OnAttachedTo(CollectionView bindable) => bindable.SizeChanged += CollectionViewOnSizeChanged;
        protected override void OnDetachingFrom(CollectionView bindable) => bindable.SizeChanged -= CollectionViewOnSizeChanged;
    }
}

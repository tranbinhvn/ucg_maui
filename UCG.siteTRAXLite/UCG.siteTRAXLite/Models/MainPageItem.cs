using UCG.siteTRAXLite.ViewModels;

namespace UCG.siteTRAXLite.Models
{
    public class MainPageItem : BindableBase
    {
        private int _count;
        public int Count
        {
            get => _count;
            set => SetProperty(ref _count, value);
        }
    }
}

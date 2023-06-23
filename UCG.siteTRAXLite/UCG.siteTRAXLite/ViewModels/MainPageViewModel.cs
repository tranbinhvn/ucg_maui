using System.Windows.Input;
using UCG.siteTRAXLite.Models;
using UCG.siteTRAXLite.Services;

namespace UCG.siteTRAXLite.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private MainPageItem? _item;

        public MainPageItem? Item
        {
            get => _item;
            set => SetProperty(ref _item, value);
        }

        public ICommand IncrementCommand { get; private set; }
        public ICommand ShowMessageCommand { get; private set; }

        public MainPageViewModel(INavigationService navigationService, MainPageItem item) : base(navigationService)
        {
            Item = item;

            IncrementCommand = new Command(() =>
            {
                if (Item != null)
                {
                    Item.Count++;
                }
            });

            ShowMessageCommand = new Command(async () =>
            {
                if (Application.Current?.MainPage != null)
                {
                    await Application.Current.MainPage.DisplayAlert("Count", Item?.Count.ToString(), "OK");
                }
            });
        }
    }
}

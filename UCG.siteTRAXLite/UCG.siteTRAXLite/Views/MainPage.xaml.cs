using UCG.siteTRAXLite.ViewModels;

namespace UCG.siteTRAXLite.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = viewModel;
        }
    }
}
using UCG.siteTRAXLite.ViewModels;

namespace UCG.siteTRAXLite.Views;

public partial class SorEformPage : ContentPage
{
    public SorEformPage(SorEformPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }

    private void Title_Tapped(object sender, TappedEventArgs e)
    {
        (BindingContext as SorEformPageViewModel).GoToLoginPageCommand.Execute(null);
    }
}
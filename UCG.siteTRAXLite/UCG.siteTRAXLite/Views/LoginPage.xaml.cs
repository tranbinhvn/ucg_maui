using UCG.siteTRAXLite.ViewModels;

namespace UCG.siteTRAXLite.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}
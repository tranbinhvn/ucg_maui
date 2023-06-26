using UCG.siteTRAXLite.ViewModels;

namespace UCG.siteTRAXLite.Views;

public partial class AppAccessPage : ContentPage
{
	public AppAccessPage(AppAccessPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
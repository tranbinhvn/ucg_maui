using UCG.siteTRAXLite.ViewModels;

namespace UCG.siteTRAXLite.Views;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(SettingsPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
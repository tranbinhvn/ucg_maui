using UCG.siteTRAXLite.ViewModels.Sections;

namespace UCG.siteTRAXLite.Views.Sections;

public partial class SorClaimsPage : MasterContentPage
{
	public SorClaimsPage(SorClaimsPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
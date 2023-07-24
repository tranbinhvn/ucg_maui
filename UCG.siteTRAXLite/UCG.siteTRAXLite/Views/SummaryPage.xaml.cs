using UCG.siteTRAXLite.ViewModels;

namespace UCG.siteTRAXLite.Views;

public partial class SummaryPage : MasterContentPage
{
	public SummaryPage(SummaryPageViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
	}
}
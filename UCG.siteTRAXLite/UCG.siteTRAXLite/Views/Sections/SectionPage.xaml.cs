using UCG.siteTRAXLite.ViewModels.Sections;

namespace UCG.siteTRAXLite.Views;

public partial class SectionPage : MasterContentPage
{
	public SectionPage(SectionPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    private void Sections_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
		var viewModel = (BindingContext as SectionPageViewModel);
		var section = e.SelectedItem;
		
		viewModel.AccessSectionCommand.Execute(section);
    }

    private void Section_Tapped(object sender, TappedEventArgs e)
    {
        var viewModel = (BindingContext as SectionPageViewModel);
        var section = (sender as StackLayout).BindingContext;

        viewModel.AccessSectionCommand.Execute(section);
    }
}
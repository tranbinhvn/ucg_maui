using UCG.siteTRAXLite.Entities.SorEforms;
using UCG.siteTRAXLite.ViewModels;
using UCG.siteTRAXLite.ViewModels.Sections;

namespace UCG.siteTRAXLite.Views;

public partial class GenericSamplePage : MasterContentPage
{
	public GenericSamplePage(GenericSamplePageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    private void responsePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        var action = picker.BindingContext;
        (BindingContext as GenericSamplePageViewModel).UpdateActionListCommand.Execute(action);
    }
}
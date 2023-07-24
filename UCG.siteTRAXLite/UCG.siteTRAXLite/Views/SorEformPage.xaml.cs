using UCG.siteTRAXLite.ViewModels;

namespace UCG.siteTRAXLite.Views;

public partial class SorEformPage : MasterContentPage
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

    private void responsePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        var action = picker.BindingContext;
        (BindingContext as SorEformPageViewModel).UpdateActionListCommand.Execute(action);
    }
}
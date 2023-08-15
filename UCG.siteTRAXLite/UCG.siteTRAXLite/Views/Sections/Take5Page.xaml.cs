using UCG.siteTRAXLite.Entities.SorEforms;
using UCG.siteTRAXLite.ViewModels.Sections;

namespace UCG.siteTRAXLite.Views.Sections;

public partial class Take5Page : MasterContentPage
{
	public Take5Page(Take5PageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }

    private void ResponseRadioSingle_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
		var radioButton = (RadioButton) sender;
        if (radioButton.IsChecked)
        {
            var selectedGroupName = radioButton.GroupName;
            var currentTab = (BindingContext as Take5PageViewModel).GetCurrentTab();
            if (currentTab != null)
                currentTab.SetResponseRadioSingle(selectedGroupName, (ResponseDataItemEntity)radioButton.BindingContext);
        }
	}

    private void BrowseFiles_Clicked(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        var action = (ActionItemEntity)btn.BindingContext;
        var currentTab = (BindingContext as Take5PageViewModel).GetCurrentTab();
        if (currentTab != null)
            currentTab.BrowseCommand.Execute(action);
    }

    private void CheckboxResponse_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var checkbox = (CheckBox) sender;
        var item = checkbox.BindingContext as ResponseDataItemEntity;
        var currentTab = (BindingContext as Take5PageViewModel).GetCurrentTab();
        var action = currentTab.Questions.FirstOrDefault(q => q.ResponseData.Contains(item));
        if(action != null)
        {
            action.Response.Value = item.Value;
            action.Response.IsChecked = e.Value;
            if (currentTab != null)
                currentTab.UpdateActionListCommand.Execute(action);
        }
    }
}
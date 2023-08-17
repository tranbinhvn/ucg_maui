using UCG.siteTRAXLite.Entities.SorEforms;
using UCG.siteTRAXLite.ViewModels.Sections;
using UraniumControls = UraniumUI.Material.Controls;

namespace UCG.siteTRAXLite.Views.Sections;

public partial class Take5Page : MasterContentPage
{
	public Take5Page(Take5PageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }

    private void RadioButtonGroupView_SelectedItemChanged(object sender, EventArgs e)
    {
        var radioButton = (UraniumControls.RadioButtonGroupView)sender;
        var action = radioButton.BindingContext as ActionItemEntity;
        var selectedItem = radioButton.SelectedItem as ResponseDataItemEntity;
        var currentTab = (BindingContext as Take5PageViewModel).GetCurrentTab();
        if (currentTab != null)
            currentTab.SetResponseRadioSingle(action, selectedItem);
    }

    private void BrowseFiles_Clicked(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        var action = (ActionItemEntity)btn.BindingContext;
        var currentTab = (BindingContext as Take5PageViewModel).GetCurrentTab();
        if (currentTab != null)
            currentTab.BrowseCommand.Execute(action);
    }

    private void CheckboxResponse_CheckedChanged(object sender, EventArgs e)
    {
        var checkbox = (UraniumControls.CheckBox)sender;
        var item = checkbox.BindingContext as ResponseDataItemEntity;
        var currentTab = (BindingContext as Take5PageViewModel).GetCurrentTab();
        if (currentTab == null)
            return;

        var action = currentTab.Questions.FirstOrDefault(q => q.ResponseData.Contains(item));
        if(action != null)
        {
            action.Response.Value = item.Value;
            action.Response.IsChecked = checkbox.IsChecked;
            currentTab.UpdateActionListCommand.Execute(action);
        }
    }

    private void RemoveFile_Clicked(object sender, EventArgs e)
    {
        var imgButton = (ImageButton)sender;
        var item = imgButton.BindingContext as QuestionImageEntity;
        var currentTab = (BindingContext as Take5PageViewModel).GetCurrentTab();
        if (currentTab == null)
            return;

        currentTab.RemoveImageCommand.Execute(item);
    }
}
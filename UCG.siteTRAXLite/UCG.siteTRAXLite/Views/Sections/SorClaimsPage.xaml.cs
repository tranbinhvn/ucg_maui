using UCG.siteTRAXLite.Entities.SorEforms;
using UCG.siteTRAXLite.ViewModels.Sections;
using UraniumControls = UraniumUI.Material.Controls;
namespace UCG.siteTRAXLite.Views.Sections;

public partial class SorClaimsPage : MasterContentPage
{
	public SorClaimsPage(SorClaimsPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    private void Unit_SelectedItemChanged(object sender, EventArgs e)
    {
        if (sender is UraniumControls.RadioButtonGroupView rbGroup)
        {
            var viewModel = BindingContext as SorClaimsPageViewModel;
            viewModel.SorsTab.SelectedUnit = rbGroup.SelectedItem as ResponseDataItemEntity;
        }
    }

    private void Sor_SelectedItemChanged(object sender, EventArgs e)
    {
        if (sender is UraniumControls.RadioButtonGroupView rbGroup && rbGroup.BindingContext is ActionItemEntity action)
        {
            action.Response = rbGroup.SelectedItem as ResponseDataItemEntity;
            action.Response.IsChecked = true;
        }
    }

    private void RemoveFile_Clicked(object sender, EventArgs e)
    {
        if (sender is ImageButton imgButton && imgButton.BindingContext is QuestionAttachmentEntity item)
        {
            var viewModel = BindingContext as SorClaimsPageViewModel;
            viewModel.UploadFilesTab.RemoveImageCommand.Execute(item);
        }
    }
}
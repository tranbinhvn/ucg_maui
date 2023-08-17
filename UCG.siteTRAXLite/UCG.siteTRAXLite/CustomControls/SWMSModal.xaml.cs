using CommunityToolkit.Maui.Views;
using UCG.siteTRAXLite.Entities.SorEforms;
using UraniumControls = UraniumUI.Material.Controls;

namespace UCG.siteTRAXLite.CustomControls;

public partial class SWMSModal : Popup
{
    public static readonly BindableProperty QuestionProperty = BindableProperty.Create(nameof(Question), typeof(ActionItemEntity), typeof(SWMSModal), null, BindingMode.TwoWay);

    public static readonly BindableProperty ResponseDataProperty = BindableProperty.Create(nameof(ResponseData), typeof(List<ResponseDataItemEntity>), typeof(SWMSModal), null, BindingMode.TwoWay);

    public ActionItemEntity Question
    {
        get => (ActionItemEntity)GetValue(QuestionProperty);
        set => SetValue(QuestionProperty, value);
    }

    public List<ResponseDataItemEntity> ResponseData
    {
        get => (List<ResponseDataItemEntity>)GetValue(ResponseDataProperty);
        set => SetValue(ResponseDataProperty, value);
    }

    public SWMSModal(ActionItemEntity question)
	{
		InitializeComponent();
        Question = question;
        var selectedItem = question.Responses.Where(r => r.IsChecked).ToList();
        ResponseData = question.ResponseData.ToList();

        foreach (var response in ResponseData)
            response.IsChecked = selectedItem.Contains(response);

        BindingContext = this;
	}

    private void OnSelectButtonClicked(object? sender, EventArgs e) 
    {
        Question.Responses = ResponseData.Where(d => d.IsChecked).ToList();
        Close(true);
    }

    private void OnCancelButtonClicked(object? sender, EventArgs e) => Close(null);

    private void CheckBox_CheckedChanged(object sender, EventArgs e)
    {
        var checkbox = sender as UraniumControls.CheckBox;
        var item = checkbox.BindingContext as ResponseDataItemEntity;
        item.IsChecked = checkbox.IsChecked;
    }
}
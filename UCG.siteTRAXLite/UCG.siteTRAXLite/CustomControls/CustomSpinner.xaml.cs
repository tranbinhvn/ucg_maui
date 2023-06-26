namespace UCG.siteTRAXLite.CustomControls;

public partial class CustomSpinner : ContentView
{
    public static readonly BindableProperty MessageProperty = BindableProperty.Create(nameof(Message), typeof(string), typeof(CustomSpinner), "", BindingMode.TwoWay);

    public string Message
    {
        get => (string)GetValue(MessageProperty);
        set => SetValue(MessageProperty, value);
    }

    public CustomSpinner(string message)
	{
		InitializeComponent();
        this.Message = message;
        BindingContext = this;
	}
}
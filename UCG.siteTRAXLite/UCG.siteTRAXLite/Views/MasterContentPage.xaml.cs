using System.Windows.Input;
using UCG.siteTRAXLite.Entities;
using UCG.siteTRAXLite.Entities.Job;
using UCG.siteTRAXLite.Helpers;
using UCG.siteTRAXLite.Models;
using UCG.siteTRAXLite.Services;

namespace UCG.siteTRAXLite.Views;

public partial class MasterContentPage : ContentPage
{
    public static readonly BindableProperty PageTitleProperty = BindableProperty.Create(nameof(PageTitle), typeof(string), typeof(MasterContentPage), "", BindingMode.TwoWay);

    public static readonly BindableProperty IsExpandedProperty = BindableProperty.Create(nameof(IsExpanded), typeof(bool), typeof(MasterContentPage), false, BindingMode.TwoWay);

    public static readonly BindableProperty ExpanderIconProperty = BindableProperty.Create(nameof(ExpanderIcon), typeof(ImageSource), typeof(MasterContentPage), ImageSource.FromFile("expander_expand_png.png"), BindingMode.TwoWay);

    public static readonly BindableProperty JobDetailProperty = BindableProperty.Create(nameof(JobDetail), typeof(JobEntity), typeof(MasterContentPage), null, BindingMode.TwoWay);

    public static readonly BindableProperty MenuItemsProperty = BindableProperty.Create(nameof(MenuItems), typeof(ConcurrentObservableCollection<MenuItemModel>), typeof(MasterContentPage), null, BindingMode.TwoWay);

    public static readonly BindableProperty GoBackCommandProperty = BindableProperty.Create(nameof(GoBackCommand), typeof(ICommand), typeof(MasterContentPage), null, BindingMode.TwoWay);

    public static readonly BindableProperty CustomFieldsProperty = BindableProperty.Create(nameof(CustomFields), typeof(ConcurrentObservableCollection<ProgramCustomFieldMobileEntity>), typeof(MasterContentPage), null, BindingMode.TwoWay);

    private readonly INavigationService _navigationService;

    public string PageTitle
    {
        get => (string)GetValue(PageTitleProperty);
        set => SetValue(PageTitleProperty, value);
    }

    public bool IsExpanded
    {
        get => (bool)GetValue(IsExpandedProperty);
        set
        {
            if (value)
            {
                ExpanderIcon = ImageSource.FromFile("expander_collapse_png.png");
                JobDetail.Site.SiteNameSize = 18;
            }
            else
            {
                ExpanderIcon = ImageSource.FromFile("expander_expand_png.png");
                JobDetail.Site.SiteNameSize = 14;
            }
            SetValue(IsExpandedProperty, value);
        }
    }

    public ImageSource ExpanderIcon
    {
        get => (ImageSource)GetValue(ExpanderIconProperty);
        set => SetValue(ExpanderIconProperty, value);
    }

    public JobEntity JobDetail
    {
        get => (JobEntity)GetValue(JobDetailProperty);
        set => SetValue(JobDetailProperty, value);
    }

    public ConcurrentObservableCollection<MenuItemModel> MenuItems
    {
        get => (ConcurrentObservableCollection<MenuItemModel>)GetValue(MenuItemsProperty);
        set => SetValue(MenuItemsProperty, value);
    }

    public ICommand GoBackCommand
    {
        get { return (ICommand)GetValue(GoBackCommandProperty); }
        set { SetValue(GoBackCommandProperty, value); }
    }

    public ConcurrentObservableCollection<ProgramCustomFieldMobileEntity> CustomFields
    {
        get => (ConcurrentObservableCollection<ProgramCustomFieldMobileEntity>)GetValue(CustomFieldsProperty);
        set => SetValue(CustomFieldsProperty, value);
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        var showMenuBtn = (Image)GetTemplateChild("showMenuBtn");
        if (showMenuBtn == null)
            return;

        var tapGesture = new TapGestureRecognizer
        {
            NumberOfTapsRequired = 1,
        };
        tapGesture.Tapped += ShowMenuBtn_Tapped;
        showMenuBtn.GestureRecognizers.Add(tapGesture);
    }

    private async void ShowMenuBtn_Tapped(object sender, TappedEventArgs e)
    {
        var showMenuBtn = (Image)GetTemplateChild("showMenuBtn");
        if (showMenuBtn == null)
            return;
        var action = await this.DisplayActionSheet("Actions", "Cancel", null, "Login");
        if (action == null || action.Equals("Cancel"))
            return;
        if (action.Equals("Login"))
            _navigationService.NavigateToPageAsync<LoginPage>();
    }

    public MasterContentPage()
    {
        _navigationService = ServiceHelper.GetService<INavigationService>();

        InitializeComponent();

        MenuItems = new ConcurrentObservableCollection<MenuItemModel>
        {
            new MenuItemModel
            {
                MenuTitle = "CLAIMS",
                MenuIcon = "icon_claims.png",
            },
            new MenuItemModel
            {
                MenuTitle = "JOBS",
                MenuIcon = "icon_jobs.png",
            },
            new MenuItemModel
            {
                MenuTitle = "SITE TRAX AIR",
                MenuIcon = "icon_sta.png",
            }
        };

        CustomFields = new ConcurrentObservableCollection<ProgramCustomFieldMobileEntity> {
            new ProgramCustomFieldMobileEntity
            {
                CustomFieldName = "Dwelling Type",
                DisplayOrder = 1,
                Value = ""
            },
            new ProgramCustomFieldMobileEntity
            {
                CustomFieldName = "Sales Order Number",
                DisplayOrder = 2,
                Value = "901228"
            },
            new ProgramCustomFieldMobileEntity
            {
                CustomFieldName = "RSP Customer Order Ref",
                DisplayOrder = 3,
                Value = "105409377"
            },
            new ProgramCustomFieldMobileEntity
            {
                CustomFieldName = "City",
                DisplayOrder = 4,
                Value = "Manukaku"
            },
            new ProgramCustomFieldMobileEntity
            {
                CustomFieldName = "Contact #1",
                DisplayOrder = 5,
                Value = "274829726"
            },
            new ProgramCustomFieldMobileEntity
            {
                CustomFieldName = "Contact #2",
                DisplayOrder = 6,
                Value = ""
            },
        };
    }
}
using System.Windows.Input;
using UCG.siteTRAXLite.Entities;
using UCG.siteTRAXLite.Models;

namespace UCG.siteTRAXLite.Views;

public partial class MasterContentPage : ContentPage
{
    public static readonly BindableProperty PageTitleProperty = BindableProperty.Create(nameof(PageTitle), typeof(string), typeof(MasterContentPage), "", BindingMode.TwoWay);

    public static readonly BindableProperty JobDetailProperty = BindableProperty.Create(nameof(JobDetail), typeof(JobDetailEntity), typeof(MasterContentPage), null, BindingMode.TwoWay);

    public static readonly BindableProperty MenuItemsProperty = BindableProperty.Create(nameof(MenuItems), typeof(ConcurrentObservableCollection<MenuItemModel>), typeof(MasterContentPage), null, BindingMode.TwoWay);

    public static readonly BindableProperty GoBackCommandProperty = BindableProperty.Create(nameof(GoBackCommand), typeof(ICommand), typeof(MasterContentPage), null, BindingMode.TwoWay);

    public string PageTitle
    {
        get => (string)GetValue(PageTitleProperty);
        set => SetValue(PageTitleProperty, value);
    }

    public JobDetailEntity JobDetail
    {
        get => (JobDetailEntity)GetValue(JobDetailProperty);
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

    public MasterContentPage()
    {
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
    }
}
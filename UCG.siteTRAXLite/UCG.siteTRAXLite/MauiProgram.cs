using Acr.UserDialogs;
using CommunityToolkit.Maui;
using FFImageLoading.Maui;
using Microsoft.Maui.LifecycleEvents;
using UCG.siteTRAXLite.Managers.Mappers;
using UCG.siteTRAXLite.Managers.SorEformManager;
using UCG.siteTRAXLite.Managers.UserDatas;
using UCG.siteTRAXLite.Managers.UserManagers;
using UCG.siteTRAXLite.Services;
using UCG.siteTRAXLite.ViewModels;
using UCG.siteTRAXLite.ViewModels.Sections;
using UCG.siteTRAXLite.Views;
using UCG.siteTRAXLite.Views.Sections;
using UCG.siteTRAXLite.WebServices.AuthenticationServices;
using UCG.siteTRAXLite.WebServices.CrewServices;
using UCG.siteTRAXLite.WebServices.DependencyServices;
using UCG.siteTRAXLite.WebServices.SorEformServices;

#if ANDROID
    using Microsoft.Maui.Controls.PlatformConfiguration;
    using Microsoft.Maui.Controls.Compatibility.Platform.Android;
#endif

namespace UCG.siteTRAXLite
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseFFImageLoading()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("FiraSans-Black.ttf", "FS-Medium");
                    fonts.AddFont("FiraSans-BlackItalic.ttf", "FS-BlackItalic");
                    fonts.AddFont("FiraSans-Bold.ttf", "FS-Bold");
                    fonts.AddFont("FiraSans-BoldItalic.ttf", "FS-BoldItalic");
                    fonts.AddFont("FiraSans-ExtraBold.ttf", "FS-ExtraBold");
                    fonts.AddFont("FiraSans-ExtraBoldItalic.ttf", "FS-ExtraBoldItalic");
                    fonts.AddFont("FiraSans-ExtraLight.ttf", "FS-ExtraLight");
                    fonts.AddFont("FiraSans-ExtraLightItalic.ttf", "FS-ExtraLightItalic");
                    fonts.AddFont("FiraSans-Italic.ttf", "FS-Italic");
                    fonts.AddFont("FiraSans-Light.ttf", "FS-Light");
                    fonts.AddFont("FiraSans-LightItalic.ttf", "FS-LightItalic");
                    fonts.AddFont("FiraSans-Medium.ttf", "FS-Medium");
                    fonts.AddFont("FiraSans-MediumItalic.ttf", "FS-MediumItalic");
                    fonts.AddFont("FiraSans-Regular.ttf", "FS-Regular");
                    fonts.AddFont("FiraSans-SemiBold.ttf", "FS-SemiBold");
                    fonts.AddFont("FiraSans-SemiBoldItalic.ttf", "FS-SemiBoldItalic");
                    fonts.AddFont("FiraSans-Thin.ttf", "FS-Thin");
                    fonts.AddFont("FiraSans-ThinItalic.ttf", "FS-ThinItalic");
                })
                .RegisterViewModels()
                .RegisterViews()
                .RegisterServices()
                .RegisterManagers()
                .RegisterModels()
                .ConfigureLifecycleEvents(events =>
                {
#if ANDROID
                    events.AddAndroid(android => android.OnApplicationCreate(app => UserDialogs.Init(app)));
#endif
                });
#if ANDROID
            Microsoft.Maui.Handlers.EditorHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
            {
                h.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
            });
#endif
            return builder.Build();
        }

        private static MauiAppBuilder RegisterManagers(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<IUserManager, UserManager>();
            mauiAppBuilder.Services.AddSingleton<ISorEformManager, SorEformManager>();

            return mauiAppBuilder;
        }

        private static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<AppShell>();
            mauiAppBuilder.Services.AddSingleton<INavigationService, NavigationService>();
            mauiAppBuilder.Services.AddSingleton(typeof(IServiceEntityMapper), typeof(ServiceEntityMapper));
            mauiAppBuilder.Services.AddSingleton<INavigationService, NavigationService>();
            mauiAppBuilder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
            mauiAppBuilder.Services.AddSingleton<IIdentityService, IdentityService>();
            mauiAppBuilder.Services.AddSingleton<ICrewService, CrewService>();
            mauiAppBuilder.Services.AddSingleton<IUserData, UserData>();
            mauiAppBuilder.Services.AddSingleton<IOpenAppService, OpenAppService>();
            mauiAppBuilder.Services.AddSingleton<IAlertService, AlertService>();
            mauiAppBuilder.Services.AddSingleton<ISorEformService, SorEformService>();
            mauiAppBuilder.Services.AddSingleton<IFileService, FileService>();

            return mauiAppBuilder;
        }

        private static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddTransient<LoginPageViewModel>();
            mauiAppBuilder.Services.AddTransient<SettingsPageViewModel>();
            mauiAppBuilder.Services.AddTransient<AppAccessPageViewModel>();
            mauiAppBuilder.Services.AddTransient<SectionPageViewModel>();
            mauiAppBuilder.Services.AddTransient<GenericSamplePageViewModel>();
            mauiAppBuilder.Services.AddTransient<Take5PageViewModel>();
            mauiAppBuilder.Services.AddTransient<SorClaimsPageViewModel>();

            return mauiAppBuilder;
        }

        private static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddTransient<LoginPage>();
            mauiAppBuilder.Services.AddTransient<SettingsPage>();
            mauiAppBuilder.Services.AddTransient<AppAccessPage>();
            mauiAppBuilder.Services.AddTransient<SectionPage>();
            mauiAppBuilder.Services.AddTransient<GenericSamplePage>();
            mauiAppBuilder.Services.AddTransient<Take5Page>();
            mauiAppBuilder.Services.AddTransient<SorClaimsPage>();

            return mauiAppBuilder;
        }

        private static MauiAppBuilder RegisterModels(this MauiAppBuilder mauiAppBuilder)
        {
            return mauiAppBuilder;
        }
    }
}
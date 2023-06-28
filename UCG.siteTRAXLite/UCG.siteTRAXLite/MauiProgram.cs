using Acr.UserDialogs;
using CommunityToolkit.Maui;
using FFImageLoading.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using System.ComponentModel;
using UCG.siteTRAXLite.Managers;
using UCG.siteTRAXLite.Managers.Mappers;
using UCG.siteTRAXLite.Managers.UserDatas;
using UCG.siteTRAXLite.Managers.UserManagers;
using UCG.siteTRAXLite.Models;
using UCG.siteTRAXLite.Services;
using UCG.siteTRAXLite.ViewModels;
using UCG.siteTRAXLite.Views;
using UCG.siteTRAXLite.WebServices.AuthenticationServices;
using UCG.siteTRAXLite.WebServices.CrewServices;

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

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private static MauiAppBuilder RegisterManagers(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<IUserManager, UserManager>();

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

            return mauiAppBuilder;
        }

        private static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddTransient<LoginPageViewModel>();
            mauiAppBuilder.Services.AddTransient<SettingsPageViewModel>();
            mauiAppBuilder.Services.AddTransient<AppAccessPageViewModel>();
            mauiAppBuilder.Services.AddTransient<SorEformPageViewModel>();

            return mauiAppBuilder;
        }

        private static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddTransient<LoginPage>();
            mauiAppBuilder.Services.AddTransient<SettingsPage>();
            mauiAppBuilder.Services.AddTransient<AppAccessPage>();
            mauiAppBuilder.Services.AddTransient<SorEformPage>();

            return mauiAppBuilder;
        }

        private static MauiAppBuilder RegisterModels(this MauiAppBuilder mauiAppBuilder)
        {
            return mauiAppBuilder;
        }
    }
}
using Microsoft.Maui.Storage;
using Newtonsoft.Json.Linq;
using UCG.siteTRAXLite.Common.Constants;

namespace UCG.siteTRAXLite.WebServices.Helper
{
    public static class Settings
    {
        private static IPreferences AppSettings
        {
            get
            {
                return Preferences.Default;
            }

        }

        #region Setting Constants

        private const string EnpointDPPSettingsKey = "enpointdppurl_key";
        private const string EnpointBMPSettingsKey = "enpointbmpurl_key";
        private const string EnpointSignalRSettingsKey = "enpointsignalurl_key";
        private const string RememberMeSettingKey = "rememberme_key";
        private const string UserNameSettingKey = "username_key";
        private const string PasswordSettingKey = "password_key";
        private const string CompanyKeySettingKey = "company_key_key";
        private const string CompanyNameSettingKey = "company_name_key";
        private const string CompanyLinkTypeSettingKey = "company_linktype_key";
        private const string LastLoginTimeKey = "logintime_key";
        private const string MaximunOfflineTimeKey = "offlinetime_key";
        private const string EndPointSettingKey = "endpointsetting_key";
        private const string TokenFireBase = "tokenfirebase_key";
        private const string SelectedCountryKey = "countryendpoint_key";
        private const string CountryKey = "country_key";
        private const string UserIdSettingKey = "userid_key";
        private const string JobReferenceKey = "jobnoteReference_key";
        private const string FileExtentionKey = "fileExtension_key";
        private const string MaterialsConfigJsonString = "materials_config_json_string";
        private const string IsEnableGeofencingKey = "is_enable_geofencting_key";
        private const string GeofencingRadiusKey = "geofencting_radius_key";
        private const string OffsiteStatusKey = "offsite_status_key";
        private const string IsEnableOffsiteModeKey = "is_enable_offsite_key";
        private const string IsuserEnableOffsiteModeKey = "is_user_enable_offsite_key";
        private const string AppTrackingTransparencyKey = "apptrackingtransparency_key";

        private const string UnleashedAPIKey_Key = "unLeashed_api_key";
        private const string UnleashedAPIID_Key = "unleashed_api_id_key";

        #endregion
        public static string EndPointSettingKeyString
        {
            get
            {
                return AppSettings.Get(EndPointSettingKey, string.Empty);
            }
            set
            {
                AppSettings.Set(EndPointSettingKey, value);
            }
        }

        public static string SelectedCountry
        {
            get
            {
                return AppSettings.Get(SelectedCountryKey, string.Empty);
            }
            set
            {
                AppSettings.Set(SelectedCountryKey, value);
            }
        }

        public static string Country
        {
            get
            {
                return AppSettings.Get(CountryKey, string.Empty);
            }
            set
            {
                AppSettings.Set(CountryKey, value);
            }
        }

        public static string EnpointIdentityUrlSetting
        {
            get
            {
                return Endpoints.GetEndpointSetting(SelectedCountry, EndPointSettingKeyString)?.EndpointIdentityURL;
            }
        }

        public static string EnpointDPPUrlSetting
        {
            get
            {
                return Endpoints.GetEndpointSetting(SelectedCountry, EndPointSettingKeyString)?.EndpointDPPBaseURL;
            }
        }

        public static string EnpointBMPUrlSetting
        {
            get
            {
                return Endpoints.GetEndpointSetting(SelectedCountry, EndPointSettingKeyString)?.EndpointBMPSignalRURL;

            }
        }

        public static string BMPSignalRUrlSetting
        {
            get
            {
                return $"{Endpoints.GetEndpointSetting(SelectedCountry, EndPointSettingKeyString)?.EndpointBMPSignalRURL}";
            }
        }

        public static string DPPSignalRUrlSetting
        {
            get
            {
                return $"{Endpoints.GetEndpointSetting(SelectedCountry, EndPointSettingKeyString)?.EndpointDPPBaseURL}/signalr";
            }
        }

        public static string EnpointUnleashedUrlSetting
        {
            get
            {
                return Endpoints.GetEndpointSetting(SelectedCountry, EndPointSettingKeyString)?.EndpointUnleashedUrl;
            }
        }

        public static string EnpointGoogleAPISetting
        {
            get
            {
                return Endpoints.GetEndpointSetting(SelectedCountry, EndPointSettingKeyString)?.EndpointGoogleAPI;
            }
        }

        public static string EnpointUnleashedAuthIdSetting
        {
            get
            {
                return AppSettings.Get(UnleashedAPIID_Key, "");
            }
            set
            {
                AppSettings.Set(UnleashedAPIID_Key, value);
            }
        }

        public static string EnpointUnleashedPrivateKeySetting
        {
            get
            {
                return AppSettings.Get(UnleashedAPIKey_Key, "");
            }
            set
            {
                AppSettings.Set(UnleashedAPIKey_Key, value);
            }
        }

        public static bool IsRememberMe
        {
            get
            {
                return AppSettings.Get(RememberMeSettingKey, false);
            }
            set
            {
                AppSettings.Set(RememberMeSettingKey, value);
            }
        }

        public static string UserNameSetting
        {
            get
            {
                return AppSettings.Get(UserNameSettingKey, string.Empty);
            }
            set
            {
                AppSettings.Set(UserNameSettingKey, value);
            }
        }

        public static string PasswordSetting
        {
            get
            {
                return AppSettings.Get(PasswordSettingKey, string.Empty);
            }
            set
            {
                AppSettings.Set(PasswordSettingKey, value);
            }
        }

        public static int UserIdSetting
        {
            get
            {
                return AppSettings.Get(UserIdSettingKey, 0);
            }
            set
            {
                AppSettings.Set(UserIdSettingKey, value);
            }
        }

        public static string CompanyKeySetting
        {
            get
            {
                return AppSettings.Get(CompanyKeySettingKey, string.Empty);
            }
            set
            {
                AppSettings.Set(CompanyKeySettingKey, value);
            }
        }

        public static Guid CompanyKeyGuidSetting
        {
            get
            {
                Guid companySettingK = Guid.Empty;
                Guid.TryParse(Settings.CompanyKeySetting, out companySettingK);
                return companySettingK;
            }
        }

        public static string CompanyNameSetting
        {
            get
            {
                return AppSettings.Get(CompanyNameSettingKey, string.Empty);
            }
            set
            {
                AppSettings.Set(CompanyNameSettingKey, value);
            }
        }

        public static string CompanyLinkTypeSetting
        {
            get
            {
                return AppSettings.Get(CompanyLinkTypeSettingKey, string.Empty);
            }
            set
            {
                AppSettings.Set(CompanyLinkTypeSettingKey, value);
            }
        }

        public static long LastLoginTime
        {
            get
            {
                return AppSettings.Get(LastLoginTimeKey, (long)0);
            }
            set
            {
                AppSettings.Set(LastLoginTimeKey, value);
            }
        }

        /// <summary>
        /// Maximum time allow user use offline mode in days
        /// </summary>
        public static int DayForOffline
        {
            get
            {
                return AppSettings.Get(MaximunOfflineTimeKey, 7);
            }
            set
            {
                AppSettings.Set(MaximunOfflineTimeKey, value);
            }
        }

        public static string TokenFireBaseString
        {
            get
            {
                return AppSettings.Get(TokenFireBase, string.Empty);
            }
            set
            {
                AppSettings.Set(TokenFireBase, value);
            }
        }

        public static string AppTrackingTransparency
        {
            get
            {
                return AppSettings.Get(AppTrackingTransparencyKey, string.Empty);
            }
            set
            {
                AppSettings.Set(AppTrackingTransparencyKey, value);
            }
        }

        public static string FileExtensions
        {
            get
            {
                return AppSettings.Get(FileExtentionKey, string.Empty);
            }
            set
            {
                AppSettings.Set(FileExtentionKey, value);
            }
        }

        public static string LoadingImageBase64String
        {
            get
            {
                return "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";
            }
        }

        public static string FileIconImageBase64String
        {
            get
            {
                return "iVBORw0KGgoAAAANSUhEUgAAAkcAAAHMCAIAAABQvpsmAAAACXBIWXMAAA7EAAAOxAGVKw4bAAAgAElEQVR42uydB3wVVdbAbzqhCZZ13aL77eqqSFFAQIT0UEIAlZ7ee2gqCEpPgICgWBBFpKWHhNAD6RSRld3Ftq66dhSlpb825Xznzn0ZnkiCSAKJnP/e39vJy2PezLw4/3fuPfdcBgRBEATxW4HRJSAIgiDIagRBEARBViMIgiAIshpBEARBkNUIgiAIshpBEARBkNUIgiAIgqxGEARBEGQ1giAIgiCrEQRBEGQ1giAIgiCrEQRBEARZjSAIgiDIagRBEARZjSAIgiDIagRBEARBViMIgiAIshpBEARBkNUIgiAIshpBEARBkNUIgiAIgqxGEARBEGQ1giAIgiCrEQRBEGQ1giAIgiCrEQRBEARZjSAIgiDIagRBEARZjSAIgiDIagRBEARBViMIgiAIshpBEARBkNUIgiAIshpBEARBkNUIgiAIgqxGEARBEGQ1giAIgqxGEARBEGQ1giAIgiCrEQRBEARZjSAIgiDIagRBEARZjSAIgiDIagRBEARBViMIgiAIshpBEARBkNUIgiAIshpBEARBkNUIgiAIgqxGEARBEGQ1giAIgqxGEARBEGQ1giAIgiCrEQRBEARZjSAIgiDIagRBEARZjSAIgiDIagRBEARBViMIgiAIshpBEARBViMIgiAIshpBEARBkNUIgiAIgqxGEARBEGQ1giAIgqxGEARBEGQ1giAIgiCrEQRBEARZjSAIgiDIagRBEARZjSAIgiDIagRBEARBViMIgiAIshpBEARBViMIgiAIshpBEARBkNUIgiAIgqxGEARBEGQ1giAIgqxGEARBEGQ1giAIgiCrEQRBEARZjSAIgiCrEQRBEARZjSAIgiDIagRBEARBViMIgiAIshpBEARBViMIgiAIshpBEARBkNUIgiAIgqxGEARBEGQ1giAIgqxGEARBEGQ1giAIgiCrEQRBEARZjSAIgiCrEQRBEARZjSAIgiDIagRBEARBViMIgiAIshpBEARBViMIgiAIshpBEARBkNUIgiAIgqxGEARBkNUIgiAIgqxGEARBEGQ1giAIgiCrEQRBEARZjSAIgiCrEQRBEARZjSAIgiDIagRBEARBViMIgiAIshpBEARBViMIgiAIshpBEARB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AKsBAABgNQAAAKwGAACA1QAAALAaAABgNQAAAKwGAACA1QAAALAaAABgNQAAAKwGAACA1QAAALAaAAAAVgMAAKwGAACA1QAAALAaAAAAVgMAAMBqAACA1QAAALAaAAAAVgMAAMBqAACA1QAAALAaAAAAVgMAAMBqAAAAWA0AALAaAAAAVgMAAMBqAAAAWA0AALAaAAAAVgMAAMBqAAAAWA0AAACrAQAAVgMAAMBqAAAAWA0AAACrAQAAYDUAAMBqAAAAWA0AAACrAQAAYDUAAMBqAAAAWA0AAACrAQAAYDUAAACsBgAAWA0AAACrAQAAYDUAAACsBgAAWA0AAACrAQAAYDUAAACsBgAAgNUAAACrAQAAYDUAAACsBgAAgNUAAACwGgAAYDUAAACsBgAAgNUAAACwGgAAYDUAAACsBgAAgNUAAACwGgAAAFYDAACsBgAAgNUAAACwGgAAAFYDAACsxlcAAABYDQAAAKsBAABgNQAAAKwGAABYDQAAAKsBAABgNQAAAKwGAACA1QAAAKsBAABgNQAAAKwGAACA1QAAAKsBAABgNQAAAKwGAACA1QAAALAaAABgNQAAAKwGAACA1QAAALAaAAAAVgMAAKwGAACA1QAAALAaAAAAVgMAAKwGAACA1QAAALAaAAAAVgMAAMBqAACA1QAAALAaAAAAVgMAAMBqAACA1QAAALAaAAAAVgMAAMBqAAAAWA0AALAaAAAAVgMAAMBqAAAAWA0AAACrAQAAVgMAAMBqAAAAWA0AAACrAQAAVgMAAMBqAAAAWA0AAACrAQAADOH/ASm56zgzA06GAAAAAElFTkSuQmCC";
            }
        }

        public static string JobReferenceSetting
        {
            get
            {
                return AppSettings.Get(JobReferenceKey, string.Empty);
            }
            set
            {
                AppSettings.Set(JobReferenceKey, value);
            }
        }

        public static bool IsEnableGeofencing
        {
            get
            {
                return AppSettings.Get(IsEnableGeofencingKey, false);
            }
            set
            {
                AppSettings.Set(IsEnableGeofencingKey, value);
            }
        }

        public static bool IsEnableOffsiteMode
        {
            get
            {
                return AppSettings.Get(IsEnableOffsiteModeKey, false);
            }
            set
            {
                AppSettings.Set(IsEnableOffsiteModeKey, value);
            }
        }

        public static bool IsUserEnableOffsiteMode
        {
            get
            {
                return AppSettings.Get(IsuserEnableOffsiteModeKey, false);
            }
            set
            {
                AppSettings.Set(IsuserEnableOffsiteModeKey, value);
            }
        }

        public static int GeofencingRadius
        {
            get
            {
                return AppSettings.Get(GeofencingRadiusKey, 0);
            }
            set
            {
                AppSettings.Set(GeofencingRadiusKey, value);
            }
        }

        public static string AppVersion { get; set; }
        public static bool AppVersionActive { get; set; }
        public static bool IsNotLatestVersion { get; set; }

        public static string OffsiteStatus
        {
            get
            {
                return AppSettings.Get(OffsiteStatusKey, string.Empty);
            }
            set
            {
                AppSettings.Set(OffsiteStatusKey, value);
            }
        }

        public static void Logout()
        {
            WebServiceBase.AccessToken = string.Empty;
        }

        public static JObject CheckUnleadConfigNotAvailable(string jsonConfig)
        {
            if (string.IsNullOrEmpty(jsonConfig)) return default;
            JObject config = JObject.Parse(jsonConfig);
            return config;
        }
    }
}


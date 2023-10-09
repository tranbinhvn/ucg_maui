namespace UCG.siteTRAXLite.Common.Constants
{
    public class Endpoints
    {
        private static Dictionary<string, CountryEndPointSetting> endpointSetting = null;
        public static Dictionary<string, CountryEndPointSetting> EndpointSettingURL
        {
            get
            {
                if (endpointSetting == null)
                {
                    endpointSetting = new Dictionary<string, CountryEndPointSetting>();

                    // NZ
                    var nzSettings = new CountryEndPointSetting("NZ");
                    nzSettings.Add("Dev",
                     new EndPointSetting(
                            name: "Dev",
                            identityURL: "https://identity.sitetrax.co.nz/",
                            endpointDPPBaseURL: "https://ci.dpp.chorus.sitetrax.co.nz/",
                            endpointBMPSignalRURL: "https://ci.chorus.sitetrax.co.nz/signalr",
                            isDefault: false
                        ));


                    endpointSetting.Add(nzSettings.CountryCode, nzSettings);
                }

                return endpointSetting;
            }
        }

        public static EndPointSetting GetEndpointSetting(string country, string endpointName)
        {
            CountryEndPointSetting countryEndPoint = null;
            if (EndpointSettingURL.ContainsKey(country))
            {
                countryEndPoint = EndpointSettingURL[country];

            }
            return countryEndPoint?.GetEnpoint(endpointName);
        }

        public static CountryEndPointSetting GetEndpointByCountry(string country)
        {
            if (EndpointSettingURL.ContainsKey(country))
            {
                return EndpointSettingURL[country];
            }
            return null;
        }

        public static IEnumerable<string> GetAllCountries()
        {
            return EndpointSettingURL.Values.ToList().Select(s => s.CountryCode);
        }

        //identiy
        public const string AuthenEndpoint = "/connect/token";
        public const string IdentityScope = "openid profile offline_access";

        // user info
        public const string UserInfoEndpoint = "/api/account/UserInfo";

        //Unleashed
        public const string UnleashedBaseUrl = "https://api.unleashedsoftware.com/";

        // MobileSettings
        public const string MobileSettingsEndpoint = "api/dpp/mobileappsettings/all";
        public const string GoogleAPIEndpoint = "https://maps.googleapis.com/maps/";
    }

    public class EndPointSetting
    {
        public string Name { get; set; }
        public string EndpointDPPBaseURL { get; set; }
        public string EndpointBMPSignalRURL { get; set; }
        public string EndpointIdentityURL { get; set; }
        public string EndpointUnleashedUrl { get; set; }
        public string EndpointGoogleAPI { get; set; }
        public bool IsDefault { get; set; }
        public EndPointSetting() { }
        public EndPointSetting(string name, string identityURL , string endpointDPPBaseURL, string endpointBMPSignalRURL, bool isDefault = false)
        {
            Name = name;
            EndpointDPPBaseURL = endpointDPPBaseURL;
            EndpointBMPSignalRURL = endpointBMPSignalRURL;
            EndpointUnleashedUrl = Endpoints.UnleashedBaseUrl;
            EndpointGoogleAPI = Endpoints.GoogleAPIEndpoint;
            EndpointIdentityURL = identityURL;
            IsDefault = isDefault;
        }
    }

    public class CountryEndPointSetting
    {
        public string CountryCode { get; set; }
        private Dictionary<string, EndPointSetting> EndpointSettingURL { get; set; }

        public CountryEndPointSetting(string countryCode)
        {
            CountryCode = countryCode;
            EndpointSettingURL = new Dictionary<string, EndPointSetting>();
        }

        public void Add(string endpointName, EndPointSetting endPointSetting)
        {
            if (EndpointSettingURL.ContainsKey(endpointName))
            {
                EndpointSettingURL[endpointName] = endPointSetting;
            }
            else
            {
                EndpointSettingURL.Add(endpointName, endPointSetting);
            }
        }

        public EndPointSetting GetEnpoint(string endpointName)
        {
            if (EndpointSettingURL.ContainsKey(endpointName))
            {
                return EndpointSettingURL[endpointName];
            }
            return EndpointSettingURL.Values.Where(s => s.IsDefault).FirstOrDefault();
        }

        public List<EndPointSetting> GetAllEnpoints()
        {
            return EndpointSettingURL.Values.ToList();
        }
    }
}

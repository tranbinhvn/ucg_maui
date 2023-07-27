using Acr.UserDialogs;
using System.Windows.Input;
using UCG.siteTRAXLite.Common.Constants;
using UCG.siteTRAXLite.Managers.Mappers;
using UCG.siteTRAXLite.Models;
using UCG.siteTRAXLite.Services;
using UCG.siteTRAXLite.WebServices.Helper;

namespace UCG.siteTRAXLite.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        #region Properties
        private long LastTimeClicked;

        private const int TimeClearInSecond = 5;

        private string _dppEndPointUrl;
        public string DPPEndPointUrl
        {
            get { return _dppEndPointUrl; }
            set { SetProperty(ref _dppEndPointUrl, value); }
        }

        private string _bmpSignalREndPointUrl;
        public string EndpointBMPSignalRURL
        {
            get { return _bmpSignalREndPointUrl; }
            set { SetProperty(ref _bmpSignalREndPointUrl, value); }
        }

        private bool _isShowEndPoint;
        public bool IsShowEndPoint
        {
            get
            {
                return _isShowEndPoint;
            }
            set
            {
                SetProperty(ref _isShowEndPoint, value);
                OnPropertyChanged(nameof(IsShowEndPoint));
            }
        }

        private int TabCount { get; set; }

        public ConcurrentObservableCollection<EndPointSetting> ListEndPointSetting { get; }

        public ConcurrentObservableCollection<string> ListCountrySetting { get; }

        private string _countrySelected;
        public string CountrySelected
        {
            get { return _countrySelected; }
            set
            {
                if (value != _countrySelected)
                {
                    if (value != null)
                        SetDataEndPoint(value);
                    SetProperty(ref _countrySelected, value);
                }
                OnPropertyChanged(nameof(CountrySelected));
            }
        }
        private EndPointSetting _itemSelected;
        public EndPointSetting ItemSelected
        {
            get { return _itemSelected; }
            set
            {
                if (value != null)
                {
                    DPPEndPointUrl = value.EndpointDPPBaseURL;
                    EndpointBMPSignalRURL = value.EndpointBMPSignalRURL;
                    SetProperty(ref _itemSelected, value);
                }
            }
        }

        private ICommand saveSettingsCommand;
        public ICommand SaveSettingsCommand
        {
            get
            {
                return this.saveSettingsCommand ?? (saveSettingsCommand = new Command(() => SaveSettings()));
            }
        }

        private ICommand showEndPointCommand;
        public ICommand ShowEndPointCommand
        {
            get
            {
                return this.showEndPointCommand ?? (showEndPointCommand = new Command(() => ShowEndPoint()));
            }
        }

        private ICommand goBackCommand;
        public ICommand GoBackCommand
        {
            get
            {
                return this.goBackCommand ?? (goBackCommand = new Command(() => GoBack()));
            }
        }
        #endregion

        public SettingsPageViewModel(
            INavigationService navigationService, 
            IAlertService alertService, 
            IOpenAppService openAppService,
            IServiceEntityMapper mapper) : base(navigationService, alertService, openAppService, mapper)
        {
            var countries = Endpoints.GetAllCountries();
            TabCount = 0;
            ListEndPointSetting = new ConcurrentObservableCollection<EndPointSetting>();
            ListCountrySetting = new ConcurrentObservableCollection<string>();
            MainThread.BeginInvokeOnMainThread(() =>
            {
                foreach (var c in countries)
                {
                    ListCountrySetting.Add(c);
                }

                foreach (var c in ListCountrySetting)
                {
                    if (c.Equals(Settings.SelectedCountry))
                    {
                        CountrySelected = c;
                        break;
                    }
                }
            });
        }

        private void SaveSettings()
        {
            if (ItemSelected == null)
            {
#if WINDOWS
                AlertService.ShowAlert(MessageStrings.NoSelectedEndpoint);
#else
                UserDialogs.Instance.Alert(MessageStrings.NoSelectedEndpoint);
#endif
                return;
            }
            else
            {
                if (Settings.EndPointSettingKeyString != ItemSelected.Name || Settings.SelectedCountry != CountrySelected)
                {
                    Settings.EndPointSettingKeyString = ItemSelected.Name;
                    Settings.SelectedCountry = CountrySelected;
                    Settings.CompanyKeySetting = string.Empty;
                    Settings.CompanyNameSetting = string.Empty;
                    Settings.CompanyLinkTypeSetting = string.Empty;
                    Settings.JobReferenceSetting = string.Empty;
                }

                NavigationService.NavigateBackAsync();
            }
        }

        private void ShowEndPoint()
        {
            var currentTicks = DateTime.Now.Ticks;
            var elapsedTicks = currentTicks - LastTimeClicked;
            var elapsedSpan = new TimeSpan(elapsedTicks);

            // should reset tab count
            if (elapsedSpan.TotalSeconds > TimeClearInSecond)
                TabCount = 0;
            LastTimeClicked = DateTime.Now.Ticks;
            TabCount++;
            if (TabCount >= 8)
            {
                IsShowEndPoint = !IsShowEndPoint;
                TabCount = 0;
            }
        }

        private void SetDataEndPoint(string countryStr)
        {
            var EndpointSettingURL = Endpoints.GetEndpointByCountry(countryStr);
            var setting = Settings.EndPointSettingKeyString;
            var strSetting = EndpointSettingURL.GetEnpoint(setting)?.Name;
            MainThread.BeginInvokeOnMainThread(() =>
            {
                ListEndPointSetting.Clear();
                foreach (var item in EndpointSettingURL.GetAllEnpoints())
                {
                    ListEndPointSetting.Add(item);
                    if (item.Name == strSetting)
                    {
                        ItemSelected = item;
                    }
                }
            });
        }

        private async void GoBack()
        {
            await NavigationService.NavigateBackAsync();
        }
    }
}

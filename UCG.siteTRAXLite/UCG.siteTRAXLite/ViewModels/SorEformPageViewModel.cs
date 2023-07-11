using Acr.UserDialogs;
using CommunityToolkit.Mvvm.Messaging;
using Newtonsoft.Json;
using System.Windows.Input;
using UCG.siteTRAXLite.Common.Constants;
using UCG.siteTRAXLite.DataContracts;
using UCG.siteTRAXLite.Entities;
using UCG.siteTRAXLite.Entities.SorEforms;
using UCG.siteTRAXLite.Extensions;
using UCG.siteTRAXLite.Managers.Mappers;
using UCG.siteTRAXLite.Managers.SorEformManager;
using UCG.siteTRAXLite.Messages;
using UCG.siteTRAXLite.Models;
using UCG.siteTRAXLite.Models.SorEformModels;
using UCG.siteTRAXLite.Services;
using UCG.siteTRAXLite.Utils;
using UCG.siteTRAXLite.Views;

namespace UCG.siteTRAXLite.ViewModels
{
    public class SorEformPageViewModel : ViewModelBase
    {
        private readonly IOpenAppService _openAppService;
        private readonly ISorEformManager _sorEformManager;
        private readonly IServiceEntityMapper _mapper;

        private string crn;
        public string CRN
        {
            get { return crn; }
            set
            {
                SetProperty(ref crn, value);
            }
        }

        private string siteName;
        public string SiteName { 
            get { return siteName; }
            set 
            {
                SetProperty(ref siteName, value);
            } 
        }
        public ConcurrentObservableCollection<string> OutcomeOptions { get; set; }
        public ConcurrentObservableCollection<ActionItemEntity> Actions { get; set; }

        private bool showQuestions;
        public bool ShowQuestions
        {
            get { return showQuestions; }
            set
            {
                SetProperty(ref showQuestions, value);
            }
        }

        private string selectedOutcomeOption;
        public string SelectedOutcomeOption
        {
            get { return selectedOutcomeOption; }
            set
            {
                if (value != null)
                {
                    ShowQuestions = true;
                    GetActionsByOutcomeName(value);
                }
                else
                {
                    ShowQuestions = false;
                }
                SetProperty(ref selectedOutcomeOption, value);
            }
        }

        private async Task GetActionsByOutcomeName(string outcome)
        {
            Actions.Clear();
            var actions = await _sorEformManager.GetActionsByOutcome(outcome);
            foreach (var item in actions)
            {
                Actions.Add(item);
            }
        }

        private ICommand goToLoginPageCommand;

        public ICommand GoToLoginPageCommand
        {
            get
            {
                return this.goToLoginPageCommand ?? (this.goToLoginPageCommand = new Command(() => GoToLoginPage()));
            }
        }

        private ICommand goToSiteTraxAirCommand;

        public ICommand GoToSiteTraxAirCommand
        {
            get
            {
                return this.goToSiteTraxAirCommand ?? (this.goToSiteTraxAirCommand = new Command(async () => await GoToSiteTraxAir()));
            }
        }

        private ICommand submitCommand;

        public ICommand SubmitCommand
        {
            get
            {
                return this.submitCommand ?? (this.submitCommand = new Command(() => Submit()));
            }
        }

        private ICommand updateActionListCommand;

        public ICommand UpdateActionListCommand
        {
            get
            {
                return this.updateActionListCommand ?? (this.updateActionListCommand = new Command<ActionItemEntity>(async(actionItemEntity) => await UpdateActionList(actionItemEntity)));
            }
        }

        public SorEformPageViewModel(INavigationService navigationService,
            IAlertService alertService,
            IOpenAppService openAppService,
            ISorEformManager sorEformManager,
            IServiceEntityMapper mapper) : base(navigationService, alertService)
        {
            _openAppService = openAppService;
            _sorEformManager = sorEformManager;
            _mapper = mapper;
            OutcomeOptions = new ConcurrentObservableCollection<string>();
            Actions = new ConcurrentObservableCollection<ActionItemEntity>();

            WeakReferenceMessenger.Default.Unregister<LaunchingAppMessage>(this);
            WeakReferenceMessenger.Default.Register<LaunchingAppMessage>(this, (r, data) =>
            {
                if (!string.IsNullOrEmpty(data.Value))
                {
                    ClearData();
                    var launchDataDto = JsonConvert.DeserializeObject<LaunchDataDTO>(data.Value);
                    var launchDataEntity = _mapper.Map<LaunchDataEntity>(launchDataDto);

                    CRN = launchDataEntity.CRN;
                    SiteName = launchDataEntity.SiteName;
                    LoadData();
                }
            });
        }

        public async override Task OnNavigatedTo()
        {
            LoadData();
        }

        private async void LoadData()
        {
            var options = await _sorEformManager.GetOutcomeNames(); ;
            foreach (var option in options)
            {
                OutcomeOptions.Add(option);
            }
        } 

        private void ClearData()
        {
            SelectedOutcomeOption = null;
            OutcomeOptions.Clear();
            Actions.Clear();
        }

        private void GoToLoginPage()
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await NavigationService.NavigateToPageAsync<LoginPage>();
            });
        }

        private void Submit()
        {

        }

        private async Task GoToSiteTraxAir()
        {
            try
            {
                var isSuccess = false;
                var text = "Data from SiteTRAX Lite";

#if ANDROID
                isSuccess = await FuncEx.ExcuteAsync(_openAppService.LaunchApp, MessageStrings.SiteTraxAir_Package_Name, text);
#elif IOS
                isSuccess = await FuncEx.ExcuteAsync(_openAppService.LaunchApp, MessageStrings.SiteTraxAir_Uri , text);
#endif

                if (!isSuccess)
                {
#if WINDOWS
                    await AlertService.ShowAlertAsync(MessageStrings.Not_Installed_App);
#else
                    await UserDialogs.Instance.AlertAsync(MessageStrings.Not_Installed_App);
#endif
                }
            }
            catch (Exception ex)
            {
#if WINDOWS
                await AlertService.ShowAlertAsync(ex.Message);
#else
                await UserDialogs.Instance.AlertAsync(ex.Message);
#endif
            }
        }

        private async Task UpdateActionList(ActionItemEntity actionItemEntity)
        {
            if (actionItemEntity != null)
            {
                var currentIndex = Actions.IndexOf(actionItemEntity);
                var currentSubList = Actions.Where(a => actionItemEntity.SubActionList.Contains(a))
                    .ToList();

                foreach(var action in currentSubList)
                {
                    Actions.Remove(action);
                }

                var newActions = actionItemEntity.SubActionList.Where(a => a.Condition.ResponseData.Equals(actionItemEntity.Responses, StringComparison.OrdinalIgnoreCase)).ToList();

                foreach (var action in newActions)
                {
                    Actions.Insert(currentIndex + 1, action);
                }
            }
        }
    }
}

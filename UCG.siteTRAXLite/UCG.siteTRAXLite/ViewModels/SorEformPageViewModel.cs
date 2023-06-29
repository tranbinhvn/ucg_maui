using Acr.UserDialogs;
using System.Windows.Input;
using UCG.siteTRAXLite.Common.Constants;
using UCG.siteTRAXLite.Extensions;
using UCG.siteTRAXLite.Models;
using UCG.siteTRAXLite.Models.SorEformModels;
using UCG.siteTRAXLite.Services;
using UCG.siteTRAXLite.Utils;

namespace UCG.siteTRAXLite.ViewModels
{
    public class SorEformPageViewModel : ViewModelBase
    {
        private readonly IOpenAppService _openAppService;

        public string CRN { get; set; }
        public string SiteName { get; set; }
        public ConcurrentObservableCollection<SorEform> SORs { get; set; }
        public ConcurrentObservableCollection<Question> Questions { get; set; }
        public List<Question> QuestionsSource { get; set; }

        public IList<object> SelectedSORs { get; set; }

        private bool showQuestions;
        public bool ShowQuestions
        {
            get { return showQuestions; }
            set
            {
                SetProperty(ref showQuestions, value);
            }
        }

        private ICommand sorSelectionChangedCommand;


        public ICommand SorSelectionChangedCommand
        {
            get
            {
                return this.sorSelectionChangedCommand ?? (this.sorSelectionChangedCommand = new Command(async () => await SorSelectionChanged()));
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

        public SorEformPageViewModel(INavigationService navigationService,
            IAlertService alertService,
            IOpenAppService openAppService) : base(navigationService, alertService)
        {
            this._openAppService = openAppService;
            this.CRN = "241226808423";
            this.SiteName = "123 FINLENNE ROAD Waipu 0582";
            this.SORs = new ConcurrentObservableCollection<SorEform>
            {
                new SorEform {Id = 1, Name = "577"},
                new SorEform {Id = 2, Name = "Z04"},
                new SorEform {Id = 3, Name = "Z320"},
            };
            this.QuestionsSource = new List<Question>
            {
                new Question { Id = 1, Description = "DETAILED EXPLANATION OF WORK YOU HAVE DONE?", Response = "" , SorId = 1},
                new Question { Id = 2, Description = "Do you have any S01 or S02 codes to be claimed?", Response = "", SorId = 1 },
                new Question { Id = 3, Description = "Are you carrying out work outside the customer's boundary?", Response = "", SorId = 1},
                new Question { Id = 4, Description = "Is there any change from the field?", Response = "" , SorId = 2},
                new Question { Id = 5, Description = "Detailed Work Description?", Response = "", SorId = 2},
                new Question { Id = 6, Description = "Site tidy, no task waste?", Response = "", SorId = 3 }
            };
            this.Questions = new ConcurrentObservableCollection<Question>();
        }

        private async Task SorSelectionChanged()
        {
            Questions.Clear();

            if (SelectedSORs != null && SelectedSORs.Count > 0)
            {
                ShowQuestions = true;
                var selectedSORs = SelectedSORs.OrderBy(s => (s as SorEform).Id);
                foreach (var sor in selectedSORs)
                {
                    var questionBySor = QuestionsSource.Where(q => q.SorId == (sor as SorEform).Id)
                        .OrderBy(q => q.Id)
                        .ToList();
                    foreach (var question in questionBySor)
                    {

                        Questions.Add(question);
                    }
                }
            }
            else
            {
                ShowQuestions = false;
            }
        }

        private void GoToLoginPage()
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await NavigationService.NavigateToAsync($"/{PageNames.LoginPage}");
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

#if ANDROID
                isSuccess = await FuncEx.ExcuteAsync(_openAppService.LaunchApp, MessageStrings.SiteTraxAir_Package_Name);
#elif IOS
                isSuccess = await FuncEx.ExcuteAsync(_openAppService.LaunchApp, $"{MessageStrings.SiteTraxAir_Package_Name}://");
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

    }
}

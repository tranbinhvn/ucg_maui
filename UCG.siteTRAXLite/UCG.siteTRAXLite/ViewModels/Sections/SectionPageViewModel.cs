using System.Windows.Input;
using UCG.siteTRAXLite.Entities.SorEforms;
using UCG.siteTRAXLite.Managers.Mappers;
using UCG.siteTRAXLite.Managers.SorEformManager;
using UCG.siteTRAXLite.Models;
using UCG.siteTRAXLite.Services;
using UCG.siteTRAXLite.Views;
using UCG.siteTRAXLite.Views.Sections;

namespace UCG.siteTRAXLite.ViewModels.Sections
{
    public class SectionPageViewModel : ViewModelBase
    {
        private readonly ISorEformManager _sorEformManager;
        public ConcurrentObservableCollection<SectionEntity> Sections { get; set; }

        public SectionPageViewModel(
            INavigationService navigationService,
            IAlertService alertService,
            IOpenAppService openAppService,
            ISorEformManager sorEformManager,
            IServiceEntityMapper mapper) : base(navigationService, alertService, openAppService, mapper)
        {
            _sorEformManager = sorEformManager;
            Sections = new ConcurrentObservableCollection<SectionEntity>();

            PageTitle = "Jobs";
        }

        private ICommand accessSectionCommand;
        public ICommand AccessSectionCommand
        {
            get
            {
                return accessSectionCommand ?? (accessSectionCommand = new Command<SectionEntity>(
                        execute: async (section) =>
                        {
                            await AccessSection(section);
                            RefreshCanExecutes();
                        },
                        canExecute: (section) => !IsCommandExecuting
                    ));
            }
        }

        public override async Task OnNavigatedTo()
        {
            await LoadSections();
        }

        public async Task LoadSections()
        {
            Sections.Clear();
            var sections = await _sorEformManager.GetSections();

            foreach (var section in sections.Where(s => !string.IsNullOrEmpty(s.Title)))
            {
                if (section.ESectionType == JobSectionType.Generic)
                {
                    section.Title = "Generic sample";
                }

                Sections.Add(section);
            }
        }

        public async Task AccessSection(SectionEntity section)
        {
            if (IsCommandExecuting)
                return;

            IsCommandExecuting = true;

            if (section.ESectionType == JobSectionType.Generic)
            {
                await NavigationService.NavigateToPageAsync<GenericSamplePage>(section);
            }
            else if (section.ESectionType == JobSectionType.Take5)
            {
                await NavigationService.NavigateToPageAsync<Take5Page>(section);
            }
            else if (section.ESectionType == JobSectionType.Claims)
            {
                await NavigationService.NavigateToPageAsync<SorClaimsPage>(section);
            }

            IsCommandExecuting = false;
        }

        private void RefreshCanExecutes()
        {
            (AccessSectionCommand as Command).ChangeCanExecute();
        }
    }
}

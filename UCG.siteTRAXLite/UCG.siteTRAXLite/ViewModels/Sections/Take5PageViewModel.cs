﻿using Acr.UserDialogs;
using System.Windows.Input;
using UCG.siteTRAXLite.Common.Constants;
using UCG.siteTRAXLite.Entities.SorEforms;
using UCG.siteTRAXLite.Managers.Mappers;
using UCG.siteTRAXLite.Managers.SorEformManager;
using UCG.siteTRAXLite.Models;
using UCG.siteTRAXLite.Models.Take5;
using UCG.siteTRAXLite.Services;

namespace UCG.siteTRAXLite.ViewModels.Sections
{
    public class Take5PageViewModel : ViewModelBase
    {
        private readonly ISorEformManager _sorEformManager;

        public ConcurrentObservableCollection<StepperEntity> Steppers { get; set; }

        private Take5TabModel controlTab;
        public Take5TabModel ControlTab
        {
            get { return controlTab; }
            set
            {
                SetProperty(ref controlTab, value);
            }
        }

        private Take5TabModel hazardTab;
        public Take5TabModel HazardTab
        {
            get { return hazardTab; }
            set
            {
                SetProperty(ref hazardTab, value);
            }
        }

        private Take5TabModel submitTab;
        public Take5TabModel SubmitTab
        {
            get { return submitTab; }
            set
            {
                SetProperty(ref submitTab, value);
            }
        }

        private StepperEntity selectedStepper;
        public StepperEntity SelectedStepper
        {
            get { return selectedStepper; }
            set 
            {
                HandleSelectedStepper(value);
                SetProperty(ref selectedStepper, value);
            }
        }

        private ICommand cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                return this.cancelCommand ?? (this.cancelCommand = new Command(async () => await Cancel()));
            }
        }

        private ICommand confirmCommand;

        public ICommand ConfirmCommand
        {
            get
            {
                return this.confirmCommand ?? (this.confirmCommand = new Command(async () => await Confirm()));
            }
        }

        public Take5PageViewModel(
            INavigationService navigationService,
            IAlertService alertService,
            IOpenAppService openAppService,
            IServiceEntityMapper mapper,
            ISorEformManager sorEformManager) : base(navigationService, alertService, openAppService, mapper)
        {
            PageTitle = "TAKE 5";

            Steppers = new ConcurrentObservableCollection<StepperEntity>();
            _sorEformManager = sorEformManager;
        }

        public async override Task OnNavigatingTo(object parameter)
        {
            Steppers.Clear();
            await LoadSteppers(parameter as SectionEntity);
        }

        public async Task LoadSteppers(SectionEntity section)
        {
            if (section != null)
            {
                var take5Steppers = await _sorEformManager.GetTake5Steppers();

                if (take5Steppers != null)
                {
                    if (take5Steppers.StepperControl != null)
                    {
                        take5Steppers.StepperControl.StepperType = StepperType.Control;
                        ControlTab = new Take5TabModel(take5Steppers.StepperControl);
                        Steppers.Add(take5Steppers.StepperControl);
                    }

                    if (take5Steppers.StepperHazard != null)
                    {
                        take5Steppers.StepperHazard.StepperType = StepperType.Hazard;
                        HazardTab = new Take5TabModel(take5Steppers.StepperHazard);
                        Steppers.Add(take5Steppers.StepperHazard);
                    }

                    if (take5Steppers.StepperSubmit != null)
                    {
                        take5Steppers.StepperSubmit.StepperType = StepperType.Submit;

                        SubmitTab = new Take5TabModel(take5Steppers.StepperSubmit);

                        Steppers.Add(take5Steppers.StepperSubmit);
                    }
                }

                SelectedStepper = Steppers.FirstOrDefault();
            }
        }

        private void HandleSelectedStepper(StepperEntity stepper)
        {
            if (stepper == null)
                return;

            ChangeTab(stepper);
        }

        private void ChangeTab(StepperEntity stepper)
        {
            ControlTab.IsVisible = stepper.StepperType == StepperType.Control;
            HazardTab.IsVisible = stepper.StepperType == StepperType.Hazard;
            SubmitTab.IsVisible = stepper.StepperType == StepperType.Submit;
        }

        public Take5TabModel GetCurrentTab()
        {
            if (SelectedStepper?.StepperType == StepperType.Control)
                return ControlTab;
            else if (SelectedStepper?.StepperType == StepperType.Hazard)
                return HazardTab;
            else if (SelectedStepper?.StepperType == StepperType.Submit)
                return SubmitTab;

            return null;
        }

        private async Task Cancel()
        {
            await NavigationService.NavigateBackAsync();
        }

        private async Task Confirm()
        {
#if WINDOWS
            await AlertService.ShowAlertAsync(MessageStrings.Submitted_Successfully);
#else
            await UserDialogs.Instance.AlertAsync(MessageStrings.Submitted_Successfully);
#endif
        }
    }
}

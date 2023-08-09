using System.Windows.Input;
using UCG.siteTRAXLite.Entities.SorEforms;
using UCG.siteTRAXLite.ViewModels;

namespace UCG.siteTRAXLite.Models.SorClaims
{
    public class ClaimSubmitTab : BindableBase
    {
        private bool isVisible;
        public bool IsVisible
        {
            get { return isVisible; }
            set { SetProperty(ref isVisible, value); }
        }

        private bool hasPrimarySor;
        public bool HasPrimarySor
        {
            get { return hasPrimarySor; }
            set { SetProperty(ref hasPrimarySor, value); }
        }

        private bool hasSecondarySors;
        public bool HasSecondarySors
        {
            get { return hasSecondarySors; }
            set { SetProperty(ref hasSecondarySors, value); }
        }

        private bool hasData;
        public bool HasData
        {
            get { return hasData; }
            set { SetProperty(ref hasData, value); }
        }

        private bool isSubmitting;
        public bool IsSubmitting
        {
            get { return isSubmitting; }
            set { SetProperty(ref isSubmitting, value); }
        }

        private bool isSubmitted;
        public bool IsSubmitted
        {
            get { return isSubmitted; }
            set 
            {
                if (value)
                    IsSubmitting = false;
                SetProperty(ref isSubmitted, value); 
            }
        }

        private StepperEntity stepperEntity;
        public StepperEntity StepperEntity
        {
            get { return stepperEntity; }
            set { SetProperty(ref stepperEntity, value); }
        }

        private ResponseDataItemEntity selectedPrimarySors;
        public ResponseDataItemEntity SelectedPrimarySors
        {
            get { return selectedPrimarySors; }
            set
            {
                SetProperty(ref selectedPrimarySors, value);
            }
        }

        private ICommand submitCommand;
        public ICommand SubmitCommand
        {
            get
            {
                return this.submitCommand ?? (this.submitCommand = new Command(async () => await Submit()));
            }
        }

        public ConcurrentObservableCollection<ActionItemEntity> SubActions { get; set; }

        public ClaimSubmitTab(StepperEntity entity)
        {
            StepperEntity = entity;

            SubActions = new ConcurrentObservableCollection<ActionItemEntity>();
        }

        public void LoadSummaryData(
            ResponseDataItemEntity selectedPrimarySor, 
            IEnumerable<ActionItemEntity> subActions)
        {
            HasPrimarySor = selectedPrimarySor != null;
            HasSecondarySors = subActions != null && subActions.Any();
            HasData = HasPrimarySor && HasSecondarySors;

            SelectedPrimarySors = selectedPrimarySor;

            SubActions.Clear();
            foreach (var actionItem in subActions)
            {
                SubActions.Add(actionItem);
            }
        }

        private async Task Submit()
        {
            IsSubmitting = true;
            await Task.Delay(2000);
            IsSubmitted = true;
        }
    }
}

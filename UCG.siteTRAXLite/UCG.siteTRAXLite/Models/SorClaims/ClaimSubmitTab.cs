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

        private StepperEntity stepperEntity;
        public StepperEntity StepperEntity
        {
            get { return stepperEntity; }
            set { SetProperty(ref stepperEntity, value); }
        }

        public ClaimSubmitTab(StepperEntity entity)
        {
            StepperEntity = entity;
        }
    }
}

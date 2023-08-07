namespace UCG.siteTRAXLite.Entities.SorEforms.Sections
{
    public class Take5StepperEntity : EntityBase
    {
        private StepperEntity stepperControl;
        public StepperEntity StepperControl
        {
            get { return stepperControl; }
            set { SetProperty(ref stepperControl, value); }
        }

        private StepperEntity stepperHazard;
        public StepperEntity StepperHazard
        {
            get { return stepperHazard; }
            set { SetProperty(ref stepperHazard, value); }
        }

        private StepperEntity stepperSubmit;

        public StepperEntity StepperSubmit
        {
            get { return stepperSubmit; }
            set { SetProperty(ref stepperSubmit, value); }
        }
    }
}

namespace UCG.siteTRAXLite.Entities.SorEforms.Sections
{
    public class SorClaimsStepperEntity : EntityBase
    {
        private StepperEntity stepperControl;
        public StepperEntity StepperControl
        {
            get { return stepperControl; }
            set { SetProperty(ref stepperControl, value); }
        }

        private StepperEntity stepperUploadFiles;
        public StepperEntity StepperUploadFiles
        {
            get { return stepperUploadFiles; }
            set { SetProperty(ref stepperUploadFiles, value); }
        }

        private StepperEntity stepperSubmit;

        public StepperEntity StepperSubmit
        {
            get { return stepperSubmit; }
            set { SetProperty(ref stepperSubmit, value); }
        }
    }
}

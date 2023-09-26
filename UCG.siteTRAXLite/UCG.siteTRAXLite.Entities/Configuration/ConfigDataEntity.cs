using UCG.siteTRAXLite.Entities.SorEforms;

namespace UCG.siteTRAXLite.Entities.Configuration
{
    public class ConfigEntity : EntityBase
    {
        public Guid ConfigK { get; set; }
        public Guid JobFK { get; set; }
        public Guid ConfigInfoFK { get; set; }
        public Guid JobTabFK { get; set; }
        public ConfigInfoEntity ConfigInfo { get; set; }

        public JobTabEntity JobTab { get; set; }
    }

    public class ConfigInfoEntity : EntityBase
    {
        public Guid ConfigInfoK { get; set; }
        public int ConfigVersion { get; set; }
    }

    public class JobTabEntity : EntityBase
    {
        public Guid JobTabK { get; set; }
        public List<SectionEntity> Sections { get; set; }
    }

    public class SectionEntity : EntityBase
    {
        public Guid SectionK { get; set; }
        public string Title { get; set; }
        public Guid JobTabFK { get; set; }
        public Guid SectionStepperFK { get; set; }
        public JobSectionType SectionType { get; set; }

        public SectionStepperEntity SectionStepper { get; set; }
    }

    public class SectionStepperEntity : EntityBase
    {
        public Guid SectionStepperK { get; set; }
        public StepperType StepperType { get; set; }

        public List<StepperEntity> Steppers { get; set; }
    }

    public class StepperEntity : EntityBase
    {
        public Guid StepperK { get; set; }
        public Guid SectionStepperFK { get; set; }
        public string Title { get; set; }

        public List<ActionEntity> Actions { get; set; }
    }

    public class ActionEntity : EntityBase
    {
        public Guid ActionK { get; set; }
        public Guid StepperFK { get; set; }
        public Guid ParentActionFK { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public SorEformsResponseType ResponseType { get; set; }

        public List<ResponseDataEntity> ResponseDatas { get; set; }

        public List<ResponseEntity> Responses { get; set; }

        public List<ActionEntity> ChildActions { get; set; }

        public PreConditionEntity PreCondition { get; set; }
    }

    public class PreConditionEntity : NameValueEntity
    {
        public Guid PreConditionK { get; set; }
        public Guid ActionFK { get; set; }
    }

    public class ResponseEntity : NameValueEntity
    {
        public Guid ResponseK { get; set; }
        public Guid ActionFK { get; set; }
    }

    public class ResponseDataEntity : NameValueEntity
    {
        public Guid ResponseDataK { get; set; }
        public Guid ActionFK { get; set; }
    }

    public class NameValueEntity : EntityBase 
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}

using SQLiteNetExtensions.Attributes;
using UCG.siteTRAXLite.DataObjects.DataObject;

namespace UCG.siteTRAXLite.DataObjects.Configuration
{
    public class ConfigDataObject : DataObjectBase<Guid>
    {
        public Guid JobFK { get; set; }
        public Guid ConfigInfoFK { get; set; }
        public Guid JobTabFK { get; set; }

        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public ConfigInfoDataObject ConfigInfo { get; set; }

        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public JobTabDataObject JobTab { get; set; }
    }

    public class ConfigInfoDataObject : DataObjectBase<Guid>
    {
        public int ConfigVersion { get; set; }
    }

    public class JobTabDataObject : DataObjectBase<Guid>
    {
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<SectionDataObject> Sections { get; set; }
    }

    public class SectionDataObject : DataObjectBase<Guid>
    {
        public string Title { get; set; }
        public byte SectionType { get; set; }
        public Guid SectionStepperFK { get; set; }
        public Guid JobTabFK { get; set; }
        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public SectionStepperDataObject SectionStepper { get; set; }
    }

    public class SectionStepperDataObject : DataObjectBase<Guid>
    {
        public byte StepperType { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<StepperDataObject> Steppers { get; set; }
    }

    public class StepperDataObject : DataObjectBase<Guid>
    {
        public Guid SectionStepperFK { get; set; }
        public string Title { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<ActionDataObject> Actions { get; set; }
    }

    public class ActionDataObject : DataObjectBase<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public byte ResponseType { get; set; }
        public Guid StepperFK { get; set; }
        public Guid ParentActionFK { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<ResponseDataDataObject> ResponseDatas { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<ResponseDataObject> Responses { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<ActionDataObject> ChildActions { get; set; }

        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public PreConditionDataObject PreCondition { get; set; }
    }

    public class PreConditionDataObject : ResponseDataObject
    {
        public Guid ActionFK { get; set; }
    }

    public class ResponseDataObject : NameValueDataObject
    {
        public Guid ActionFK { get; set; }
    }

    public class ResponseDataDataObject : NameValueDataObject
    {
        public Guid ActionFK { get; set; }
    }

    public class NameValueDataObject : DataObjectBase<Guid>
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}

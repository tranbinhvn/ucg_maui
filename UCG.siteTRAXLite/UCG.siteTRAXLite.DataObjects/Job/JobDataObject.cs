using SQLiteNetExtensions.Attributes;
using UCG.siteTRAXLite.DataObjects.DataObject;
using UCG.siteTRAXLite.DataObjects.Site;

namespace UCG.siteTRAXLite.DataObjects.Job
{
    public class JobDataObject : DataObjectBase<Guid>
    {
        public Guid SiteFK { get; set; }
        public string Age { get; set; }
        public string JobType { get; set; }
        public bool IsActivated { get; set; }
        public bool IsDeleted { get; set; }
        public string WorkflowStatus { get; set; }
        public string JobDescription { get; set; }
        public DateTime PlannedStartDate { get; set; }
        public DateTime PlannedEndDate { get; set; }
        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public SiteDataObject Site { get; set; }
    }
}

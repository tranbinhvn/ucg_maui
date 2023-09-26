using UCG.siteTRAXLite.Entities.Site;

namespace UCG.siteTRAXLite.Entities.Job
{
    public class JobEntity : EntityBase
    {
        public Guid JobK { get; set; }
        public Guid SiteFK { get; set; }
        public string Age { get; set; }
        public string JobType { get; set; }
        public bool IsActivated { get; set; }
        public bool IsDeleted { get; set; }
        public string WorkflowStatus { get; set; }
        public string JobDescription { get; set; }
        public DateTime PlannedStartDate { get; set; }
        public DateTime PlannedEndDate { get; set; }
        public SiteEntity Site { get; set; }
    }
}

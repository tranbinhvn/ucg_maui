namespace UCG.siteTRAXLite.Entities
{
    public class JobDetailEntity : EntityBase
    {
        private string crn;
        public string CRN
        {
            get
            {
                return crn;
            }
            set
            {
                SetProperty(ref crn, value);
            }
        }

        private string siteName;
        public string SiteName
        {
            get
            {
                return siteName;
            }
            set
            {
                SetProperty(ref siteName, value);
            }
        }

        private double siteNameSize;
        public double SiteNameSize
        {
            get
            {
                return siteNameSize;
            }
            set
            {
                SetProperty(ref siteNameSize, value);
            }
        }

        public string JobType { get; set; }
        public DateTime PlannedEndDate { get; set; }
        public DateTime PlannedStartDate { get; set; }
        public string WorkflowStatus { get; set; }
        public string Age { get; set; }
    }
}

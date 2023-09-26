namespace UCG.siteTRAXLite.Entities.Site
{
    public class SiteEntity : EntityBase
    {
        public Guid SiteK { get; set; }
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
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string Suburb { get; set; }
        public string PostalCode { get; set; }
        public bool IsDeleted { get; set; }
        public string SiteAddress { get; set; }
    }
}

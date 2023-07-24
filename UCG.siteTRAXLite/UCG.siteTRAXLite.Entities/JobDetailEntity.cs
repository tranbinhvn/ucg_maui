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
    }
}

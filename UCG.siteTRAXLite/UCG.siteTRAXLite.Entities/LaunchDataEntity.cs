namespace UCG.siteTRAXLite.Entities
{
    public class LaunchDataEntity : EntityBase
    {
        private JobDetailEntity jobDetail;
        public JobDetailEntity JobDetail { 
            get 
            { 
                return jobDetail; 
            }
            set
            {
                SetProperty(ref jobDetail, value);
            }
        }
    }
}

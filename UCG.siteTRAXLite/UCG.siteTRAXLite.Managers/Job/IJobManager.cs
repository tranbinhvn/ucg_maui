using UCG.siteTRAXLite.Entities.Job;
using UCG.siteTRAXLite.Entities.Site;

namespace UCG.siteTRAXLite.Managers.SiteAndJob
{
    public interface IJobManager
    {
        bool SaveSiteAndJob(JobEntity job);
        JobEntity LoadJobInfo();
    }
}

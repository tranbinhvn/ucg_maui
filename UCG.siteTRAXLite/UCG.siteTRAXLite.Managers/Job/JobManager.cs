using Microsoft.Maui.Networking;
using UCG.siteTRAXLite.DataObjects.Job;
using UCG.siteTRAXLite.DataObjects.Site;
using UCG.siteTRAXLite.Entities.Job;
using UCG.siteTRAXLite.Managers.Mappers;
using UCG.siteTRAXLite.Managers.SiteAndJob;
using UCG.siteTRAXLite.Repositories.Job;
using UCG.siteTRAXLite.Repositories.Site;

namespace UCG.siteTRAXLite.Managers.Job
{
    public class JobManager : ManagerBase, IJobManager
    {
        private readonly IJobRepository _jobRepo;
        private readonly ISiteRepository _siteRepo;
        public JobManager(
            IConnectivity connectivity,
            IServiceEntityMapper mapper,
            IJobRepository jobRepo,
            ISiteRepository siteRepo) : base(connectivity, mapper)
        {
            _jobRepo = jobRepo;
            _siteRepo = siteRepo;
        }

        public JobEntity LoadJobInfo()
        {
            var job = (from j in _jobRepo.All()
                       join s in _siteRepo.All() on j.SiteFK equals s.ServerK
                       select new JobDataObject
                       {
                           ServerK = j.ServerK,
                           JobType = j.JobType,
                           WorkflowStatus = j.WorkflowStatus,
                           Age = j.Age,
                           PlannedEndDate = j.PlannedEndDate,
                           PlannedStartDate = j.PlannedStartDate,
                           Site = new SiteDataObject
                           {
                               ServerK = s.ServerK,
                               SiteName = s.SiteName,
                               CRN = s.CRN
                           }
                       }).FirstOrDefault();

            return Mapper.Map<JobEntity>(job);
        }

        public bool SaveSiteAndJob(JobEntity job)
        {
            if (job == null) return false;
            if (job.Site != null)
            {
                var siteDb = Mapper.Map<SiteDataObject>(job.Site);
                _siteRepo.Save(siteDb);
            }
            var jobDb = Mapper.Map<JobDataObject>(job);
            _jobRepo.Save(jobDb);

            return true;
        }
    }
}

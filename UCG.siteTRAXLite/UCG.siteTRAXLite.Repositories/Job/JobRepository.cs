using UCG.siteTRAXLite.DataObjects.Job;
using UCG.siteTRAXLite.Repositories.Database;

namespace UCG.siteTRAXLite.Repositories.Job
{
    public class JobRepository : Repository<JobDataObject, Guid>, IJobRepository
    {
        public JobRepository(IMobileDatabase db) : base(db)
        {
        }
    }
}

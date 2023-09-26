using UCG.siteTRAXLite.DataObjects.Site;
using UCG.siteTRAXLite.Repositories.Database;

namespace UCG.siteTRAXLite.Repositories.Site
{
    public class SiteRepository : Repository<SiteDataObject, Guid>, ISiteRepository
    {
        public SiteRepository(IMobileDatabase db) : base(db)
        {
        }
    }
}

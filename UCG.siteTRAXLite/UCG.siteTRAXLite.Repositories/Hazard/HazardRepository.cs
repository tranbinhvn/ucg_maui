using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCG.siteTRAXLite.DataObjects;
using UCG.siteTRAXLite.Repositories.Database;

namespace UCG.siteTRAXLite.Repositories.Hazard
{
    public class HazardRepository : Repository<HazardDataObject, Guid>, IHazardRepository
    {
        public HazardRepository(IMobileDatabase db) : base(db) { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCG.siteTRAXLite.DataObjects;

namespace UCG.siteTRAXLite.Repositories.Hazard
{
    public interface IHazardRepository : IRepository<HazardDataObject, Guid>
    {
        
    }
}

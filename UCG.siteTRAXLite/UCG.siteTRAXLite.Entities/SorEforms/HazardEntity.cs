using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCG.siteTRAXLite.Entities.SorEforms
{
    public class HazardEntity: IHasLocaIdEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string SiteAddress { get; set; }
        public int LocalId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCG.siteTRAXLite.DataObjects.DataObject;

namespace UCG.siteTRAXLite.DataObjects
{
    public class HazardDataObject : DataObjectBase<Guid>
    {
        public string SiteAddress { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

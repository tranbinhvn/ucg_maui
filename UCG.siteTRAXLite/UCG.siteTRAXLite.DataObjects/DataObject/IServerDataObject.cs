using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCG.siteTRAXLite.DataObjects.DataObject
{
    public interface IServerDataObject<T> : IDataObjectBase
    {
        T ServerK { get; set; }
    }
}

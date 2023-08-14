using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCG.siteTRAXLite.DataObjects.DataObject
{
    public class DataObjectBase<T> : IDataObjectBase, IServerDataObject<T>
    {
        [PrimaryKey, AutoIncrement]
        public virtual int ID { get; set; }
        public T ServerK { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCG.siteTRAXLite.DataObjects.DataObject;
using UCG.siteTRAXLite.Repositories;

namespace UCG.siteTRAXLite.Managers
{
    public interface ILocalManagerBase<TEntity, TDataObject, TRepo> : IManagerBase
       where TEntity : class
       where TDataObject : class, IDataObjectBase, IServerDataObject<Guid>, new()
       where TRepo : IRepository<TDataObject, Guid>
    {
        List<TDataObject> InsertToDb(List<TEntity> entities);
        void InsertDataObjectToDb(List<TDataObject> dataObjects);
    }
}

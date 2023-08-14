using Microsoft.Maui.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCG.siteTRAXLite.DataObjects.DataObject;
using UCG.siteTRAXLite.Entities;
using UCG.siteTRAXLite.Managers.Mappers;
using UCG.siteTRAXLite.Repositories;

namespace UCG.siteTRAXLite.Managers
{
    public class LocalManagerBase<TEntity, TDataObject, TRepo> : ManagerBase, ILocalManagerBase<TEntity, TDataObject, TRepo>
       where TEntity : class
       where TDataObject : class, IDataObjectBase, IServerDataObject<Guid>, new()
       where TRepo : IRepository<TDataObject, Guid>
    {
        protected TRepo repository;
        public LocalManagerBase(IConnectivity connectivity, IServiceEntityMapper mapper, TRepo repo)
            : base(connectivity, mapper)
        {
            this.repository = repo;
        }
        public List<TDataObject> InsertToDb(List<TEntity> entities)
        {

            if (entities.Count == 0)
                return null;

            var dataObjects = Mapper.Map<List<TDataObject>>(entities);
            repository.SaveOrUpdate(dataObjects);
            if (typeof(IHasLocaIdEntity).IsAssignableFrom(typeof(TEntity)))
            {
                for (int i = 0; i < entities.Count; i++)
                {
                    var locaIdEntity = entities[i] as IHasLocaIdEntity;
                    locaIdEntity.LocalId = dataObjects[i].ID;
                }
            }
            return dataObjects;
        }

        public void InsertDataObjectToDb(List<TDataObject> dataObjects)
        {
            InsertDataObjectToLocalGeneric<TDataObject, TRepo>(dataObjects, repository);
        }
    }
}

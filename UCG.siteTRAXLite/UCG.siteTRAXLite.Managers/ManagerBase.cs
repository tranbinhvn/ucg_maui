using Microsoft.Maui.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCG.siteTRAXLite.DataContracts;
using UCG.siteTRAXLite.DataObjects.DataObject;
using UCG.siteTRAXLite.Entities;
using UCG.siteTRAXLite.Managers.Mappers;
using UCG.siteTRAXLite.Repositories;
using UCG.siteTRAXLite.WebServices.Exceptions;

namespace UCG.siteTRAXLite.Managers
{
    public class ManagerBase : IManagerBase
    {
        #region Fields

        protected readonly IServiceEntityMapper Mapper;
        protected readonly IConnectivity Connectivity;

        #endregion Fields

        #region Constructor

        public ManagerBase(IConnectivity connectivity, IServiceEntityMapper mapper)
        {
            this.Connectivity = connectivity;
            this.Mapper = mapper;
        }
        #endregion Constructor

        #region PublicMethods
        public bool IsInternetAvailable()
        {
            return Connectivity.NetworkAccess == NetworkAccess.Internet;
        }

        public void CheckIfNetWorkOk(MessageResponse data)
        {
            if (!data.IsSuccess)
            {
                throw new NetworkException(data.ErrorDescription, data.Code);
            }
        }

        public virtual bool IsAvailaleInServer()
        {
            return true;
        }

        public virtual void ResetConnect() { }

        public List<TDataObject> InsertToLocal<TEntity, TDataObject, TRepos>(List<TEntity> entities, TRepos repo)
           where TEntity : class
           where TDataObject : class, IDataObjectBase, IServerDataObject<Guid>, new()
           where TRepos : IRepository<TDataObject, Guid>
        {
            if (entities.Count == 0)
                return null;

            var dataObjects = Mapper.Map<List<TDataObject>>(entities);
            repo.SaveOrUpdate(list: dataObjects, ignoreUpdate: false, updataAction: null, existItems: null, inTransaction: true, shouldDeleteNonExistServerK: false);
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

        protected List<TDataObject> InsertDataObjectToLocalGeneric<TDataObject, TRepos>(List<TDataObject> dataObjects, TRepos repo)
            where TDataObject : class, IDataObjectBase, IServerDataObject<Guid>, new()
            where TRepos : IRepository<TDataObject, Guid>
        {
            if (dataObjects.Count == 0)
                return null;
            repo.SaveOrUpdate(dataObjects);
            return dataObjects;
        }

        #endregion PublicMethods
    }
}

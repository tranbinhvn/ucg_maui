using Microsoft.Maui.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCG.siteTRAXLite.DataContracts;
using UCG.siteTRAXLite.Managers.Mappers;
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
    }
}

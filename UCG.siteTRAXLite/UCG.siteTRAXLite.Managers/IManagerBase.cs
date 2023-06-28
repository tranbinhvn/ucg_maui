using UCG.siteTRAXLite.DataContracts;

namespace UCG.siteTRAXLite.Managers
{
    public interface IManagerBase
    {
        bool IsInternetAvailable();
        void CheckIfNetWorkOk(MessageResponse data);
        bool IsAvailaleInServer();
        void ResetConnect();
    }
}

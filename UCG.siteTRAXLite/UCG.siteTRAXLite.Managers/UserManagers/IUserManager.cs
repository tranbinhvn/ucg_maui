using UCG.siteTRAXLite.Entities;

namespace UCG.siteTRAXLite.Managers.UserManagers
{
    public interface IUserManager
    {
        Task<UserInfoEntity> GetUserInfo(bool onConnectedState = true);
    }
}

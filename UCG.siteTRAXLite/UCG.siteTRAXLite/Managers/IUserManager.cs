using UCG.siteTRAXLite.Entities;

namespace UCG.siteTRAXLite.Managers
{
    public interface IUserManager
    {
        Task<UserInfoEntity> GetUserInfo(bool onConnectedState = true);
    }
}

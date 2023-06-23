using UCG.siteTRAXLite.Entities;

namespace UCG.siteTRAXLite.Managers
{
    public interface IUserData
    {
        UserInfoEntity GetUserInfo();

        void SetUserInfo(UserInfoEntity userInfo);
    }
}

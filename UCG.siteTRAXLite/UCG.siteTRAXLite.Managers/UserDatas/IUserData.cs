using UCG.siteTRAXLite.Entities;

namespace UCG.siteTRAXLite.Managers.UserDatas
{
    public interface IUserData
    {
        UserInfoEntity GetUserInfo();

        void SetUserInfo(UserInfoEntity userInfo);
    }
}

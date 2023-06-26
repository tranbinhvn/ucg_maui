using UCG.siteTRAXLite.Entities;

namespace UCG.siteTRAXLite.Managers
{
    public class UserData : IUserData
    {
        private UserInfoEntity _userInfo;

        public UserInfoEntity GetUserInfo()
        {
            return _userInfo;
        }

        public void SetUserInfo(UserInfoEntity userInfo)
        {
            _userInfo = userInfo;
        }
    }
}

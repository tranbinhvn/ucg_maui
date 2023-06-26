using UCG.siteTRAXLite.Entities;
using UCG.siteTRAXLite.Mappers;
using UCG.siteTRAXLite.WebServices.CrewServices;

namespace UCG.siteTRAXLite.Managers
{
    public class UserManager : ManagerBase, IUserManager
    {
        private readonly ICrewService _crewService;

        public UserManager(IConnectivity connectivity,
            IServiceEntityMapper mapper,
            ICrewService crewService) : base(connectivity, mapper)
        {
            _crewService = crewService;
        }

        public async Task<UserInfoEntity> GetUserInfo(bool onConnectedState = true)
        {
            if (!onConnectedState)
            {
                return null;
            }

            var userInfo = await _crewService.GetUserInfo();
            CheckIfNetWorkOk(userInfo.Message);
            var userInfoEntities = Mapper.Map<UserInfoEntity>(userInfo.Result);

            return userInfoEntities;
        }
    }
}

using UCG.siteTRAXLite.DataContracts;
using UCG.siteTRAXLite.DataContracts.UserInfoContracts;

namespace UCG.siteTRAXLite.WebServices.CrewServices
{
    public interface ICrewService
    {
        Task<ResponseResult<UserInfoDto>> GetUserInfo();
    }
}

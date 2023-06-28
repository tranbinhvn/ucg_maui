using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCG.siteTRAXLite.Common.Constants;
using UCG.siteTRAXLite.DataContracts;
using UCG.siteTRAXLite.DataContracts.UserInfoContracts;

namespace UCG.siteTRAXLite.WebServices.CrewServices
{
    public class CrewService: WebServiceBase, ICrewService
    {
        public CrewService() : base(EndPointType.DPPEndpoint) { }

        public async Task<ResponseResult<UserInfoDto>> GetUserInfo()
        {
            return await GetRequestWithHandleErrorAsync<UserInfoDto>(Endpoints.UserInfoEndpoint);
        }
    }
}

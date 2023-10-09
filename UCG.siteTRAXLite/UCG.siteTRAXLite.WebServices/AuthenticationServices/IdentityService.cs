using UCG.siteTRAXLite.Common.Constants;
using UCG.siteTRAXLite.DataContracts;
using UCG.siteTRAXLite.DataContracts.AuthenContracts;
using UCG.siteTRAXLite.WebServices.Exceptions;

namespace UCG.siteTRAXLite.WebServices.AuthenticationServices
{
    public class IdentityService : WebServiceBase, IIdentityService
    {
        public IdentityService() : base(EndPointType.IdentityEndpoint) { }

        public async Task<MessageResponse> Login(string username, string password, string extraInfoJson = "")
        {
            MessageResponse result = new MessageResponse();
            var reqData = new AuthenRequestData()
            {
                GrantType = "password",
                ClientID = ClientID,
                ClientSecret = ClientSecret,
                Password = password,
                Username = username,
                Scope = Endpoints.IdentityScope
            };
            try
            {
                var authenResponse = await PostFormRequestAsync<AuthenRequestData, AuthenResponse>(Endpoints.AuthenEndpoint, reqData);
                WebServiceBase.AccessToken = authenResponse.AccessToken;
                result.Code = ResponseCode.SUCCESS;
            }
            catch (NetworkException e)
            {
                await UpdateMessageWhenFailed(result, e);
            }
            catch (Exception ex)
            {
                await UpdateMessageWhenFailed(result, new NetworkException(ResponseCode.CONNECTIONERROR));
            }
            return result;
        }
    }
}

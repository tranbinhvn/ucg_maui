using UCG.siteTRAXLite.DataContracts;

namespace UCG.siteTRAXLite.WebServices.AuthenticationServices
{
    public interface IIdentityService : IWebServiceBase
    {
        Task<MessageResponse> Login(string username, string password, string extraInfoJson = "");
    }
}

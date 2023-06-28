using UCG.siteTRAXLite.DataContracts;
using UCG.siteTRAXLite.WebServices.Exceptions;

namespace UCG.siteTRAXLite.WebServices
{
    public interface IWebServiceBase
    {
        void CreateHttpClient();
        TResult DeserializeObjectAsync<TResult>(string preloadedJson);
        Task<TResult> GetRequestAsync<TResult>(string requestUri);
        Task<ResponseResult<TResult>> GetRequestWithHandleErrorAsync<TResult>(string requestUri);
        Task<MessageResponse> GetRequestWithoutResultAsync(string requestUri);
        Task<TResult> PostRequestAsync<T, TResult>(string requestUri, T content);
        Task<ResponseResult<TResult>> PostRequestWithHandleErrorAsync<T, TResult>(string requestUri, T content);
        Task<MessageResponse> PostRequestWithoutResultAsync<T>(string requestUri, T content);
        Task<MessageResponse> PostRequestWithoutResultWithHandleErrorAsync(string requestUri);
        Task<TResult> PostRequestAsync<TResult>(string requestUri);
        Task<ResponseResult<TResult>> PostRequestWithHandleErrorAsync<TResult>(string requestUri);
        Task<TResult> PutRequestAsync<T, TResult>(string requestUri, T content);
        Task<TResult> PostFormRequestAsync<T, TResult>(string requestUri, T content) where T : class;
        Task<MessageResponse> PostFormRequestWithoutResultWithHandleErrorAsync<T>(string requestUri, T content) where T : class;
        Task<MessageResponse> PostRequestWithHandleErrorAsyncWithoutResult<T>(string requestUri, T content);
        Task UpdateMessageWhenFailed(MessageResponse result, NetworkException e);
        bool ApiExistInServer();
        bool IsEarlierVersion(string version);
        bool IsEarlier31Version();

        void UpdateUnleashKey();
    }
}

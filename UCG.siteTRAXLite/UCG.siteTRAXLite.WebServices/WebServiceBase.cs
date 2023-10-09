using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.ComponentModel;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using UCG.siteTRAXLite.DataContracts;
using UCG.siteTRAXLite.DataContracts.ModelStateContracts;
using UCG.siteTRAXLite.WebServices.Exceptions;
using UCG.siteTRAXLite.WebServices.Helper;

namespace UCG.siteTRAXLite.WebServices
{
    public class WebServiceBase : IWebServiceBase, IDisposable
    {
        class ApiVersionHttpClientHandler : HttpClientHandler
        {
            private bool CheckApiVersion { get; set; }
            private string ApiVersion { get; set; }
            public ApiVersionHttpClientHandler(bool checkApiVersion, string apiVersion)
            {
                CheckApiVersion = checkApiVersion;
                ApiVersion = apiVersion;
            }

            protected override async Task<HttpResponseMessage> SendAsync
                (HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
            {
                if (CheckApiVersion && !string.IsNullOrEmpty(ApiVersion)
                    && ApiVersion.CompareTo(CurrentApiVersion) > 0)
                    throw new NetworkException(ResponseCode.APIWRONGVERION);
                var response = await base.SendAsync(request, cancellationToken);
                return response;
            }
        }

        private readonly JsonSerializerSettings _serializerSettings;
        public string APIBaseURL;
        public static string AccessToken { get; set; }
        public static string UnleasedAuthId { get; set; }
        public static string UnleasedPrivateKey { get; set; }
        public static string CurrentApiVersion { get; set; } = string.Empty;
        public string ApiVersion { get; }
        public bool CheckApiVersion { get; set; }
        public string ClientID { get; } = "sitetrax_maui";
        public string ClientSecret { get; } = "ZYCaJ962CTVDkpPLZX";

        private static string DataFormat = "application/json";
        private Uri APIBaseAddress;
        private TimeSpan WebRequestTimeout = TimeSpan.FromSeconds(5000);
        private HttpClient httpClient;
        public HttpClient HttpClient
        {
            get
            {
                if (httpClient == null)
                {
                    CreateHttpClient();
                }
                return httpClient;
            }
        }

        public void CreateHttpClient()
        {
            try
            {
                APIBaseURL = string.Empty;
                switch (EndpointType)
                {
                    case EndPointType.DPPEndpoint:
                        APIBaseURL = Settings.EnpointDPPUrlSetting;
                        break;
                    case EndPointType.UnleasedEndpoint:
                        APIBaseURL = Settings.EnpointUnleashedUrlSetting;
                        UnleasedAuthId = Settings.EnpointUnleashedAuthIdSetting;
                        UnleasedPrivateKey = Settings.EnpointUnleashedPrivateKeySetting;
                        break;
                    case EndPointType.GoogleAPIEndPoint:
                        APIBaseURL = Settings.EnpointGoogleAPISetting;
                        break;
                    case EndPointType.IdentityEndpoint:
                        APIBaseURL = Settings.EnpointIdentityUrlSetting;
                        break;
                }
                APIBaseAddress = new Uri(APIBaseURL);
                if (httpClient != null)
                    httpClient.Dispose();
                if (handler != null)
                    handler.Dispose();

                handler = new ApiVersionHttpClientHandler(CheckApiVersion, ApiVersion);

                this.httpClient = new HttpClient(handler, false) { BaseAddress = APIBaseAddress };

                this.httpClient.Timeout = WebRequestTimeout;
                this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(DataFormat));
            }
            catch (Exception)
            {
                this.httpClient = null;
            }
        }

        public bool ApiExistInServer()
        {
            return string.IsNullOrEmpty(ApiVersion)
                    || ApiVersion.CompareTo(CurrentApiVersion) <= 0;
        }

        protected readonly EndPointType EndpointType;
        HttpClientHandler handler;
        public WebServiceBase(EndPointType type, string apiVersion = "", bool shouldCheckVersion = true)
        {
            EndpointType = type;
            ApiVersion = apiVersion;
            CheckApiVersion = shouldCheckVersion;

            CreateHttpClient();
            _serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ObjectCreationHandling = ObjectCreationHandling.Auto,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                Formatting = Formatting.Indented
            };
            _serializerSettings.Converters.Add(new StringEnumConverter());
        }

        public async Task<TResult> SendRequestAsync<TResult>(HttpRequestMessage requestMessage, string requestUri, string query)
        {

            if (httpClient == null)
                CreateHttpClient();

            requestMessage.RequestUri = new Uri(string.Format("{0}{1}{2}", httpClient.BaseAddress, requestUri, query));
            requestMessage.Headers.Add("Accept", "application/json");
            requestMessage.Headers.Add("api-auth-id", UnleasedAuthId);
            requestMessage.Headers.Add("api-auth-signature", GetUnleashedApiSignature((query.StartsWith("?") ? query.Trim('?') : ""), UnleasedPrivateKey));
            using (var requestResponse = await this.HttpClient.SendAsync(requestMessage))
            {
                // check request here
                return await HandleResponse<TResult>(requestResponse);
            }
        }

        private static string GetUnleashedApiSignature(string args, string privatekey)
        {
            var encoding = new System.Text.UTF8Encoding();
            byte[] key = encoding.GetBytes(privatekey);
            var myhmacsha256 = new HMACSHA256(key);
            byte[] hashValue = myhmacsha256.ComputeHash(encoding.GetBytes(args));
            string hmac64 = Convert.ToBase64String(hashValue);
            myhmacsha256.Clear();
            return hmac64;
        }

        public async Task<TResult> GetRequestAsync<TResult>(string requestUri)
        {
            //Authentication : use if ever
            if (!string.IsNullOrEmpty(AccessToken))
            {
                this.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
            }

            using (var requestResponse = await this.HttpClient.GetAsync(requestUri))
            {
                // check request here
                return await HandleResponse<TResult>(requestResponse);
            }
        }

        public async Task<TResult> PutRequestAsync<T, TResult>(string requestUri, T content)
        {
            if (!string.IsNullOrEmpty(AccessToken))
            {
                this.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
            }

            var jsonString = JsonConvert.SerializeObject(content);

            var stringJsonContent = new StringContent(jsonString, Encoding.UTF8, DataFormat);

            using (var requestResponse = await this.HttpClient.PutAsync(requestUri, stringJsonContent))
            {
                return await HandleResponse<TResult>(requestResponse);
            }
        }

        public async Task<TResult> PostRequestAsync<T, TResult>(string requestUri, T content)
        {
            if (!string.IsNullOrEmpty(AccessToken))
            {
                this.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
            }

            var jsonString = JsonConvert.SerializeObject(content);

            var stringJsonContent = new StringContent(jsonString, Encoding.UTF8, DataFormat);

            using (var requestResponse = await this.HttpClient.PostAsync(requestUri, stringJsonContent))
            {
                return await HandleResponse<TResult>(requestResponse);
            }
        }

        public async Task<TResult> PostRequestAsync<TResult>(string requestUri)
        {
            if (!string.IsNullOrEmpty(AccessToken))
            {
                this.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
            }

            using (var requestResponse = await this.HttpClient.PostAsync(requestUri, null))
            {
                return await HandleResponse<TResult>(requestResponse);
            }
        }

        public async Task<TResult> PostFormRequestAsync<T, TResult>(string requestUri, T content) where T : class
        {
            if (!string.IsNullOrEmpty(AccessToken))
            {
                this.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
            }
            var dict = DictionaryFromType(content);
            var reqContent = new FormUrlEncodedContent(dict);

            using (var requestResponse = await this.HttpClient.PostAsync(requestUri, reqContent))
            {
                return await HandleResponse<TResult>(requestResponse);
            }
        }

        public async Task<MessageResponse> PostFormRequestWithoutResultWithHandleErrorAsync<T>(string requestUri, T content) where T : class
        {
            var result = new MessageResponse();
            try
            {
                if (!string.IsNullOrEmpty(AccessToken))
                {
                    this.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
                }
                var dict = DictionaryFromType(content);
                var reqContent = new FormUrlEncodedContent(dict);

                using (var requestResponse = await this.HttpClient.PostAsync(requestUri, reqContent))
                {
                    await CheckIfResponseSuccess(requestResponse);
                }
                result.Code = ResponseCode.SUCCESS;
            }
            catch (NetworkException ex)
            {
                await UpdateMessageWhenFailed(result, ex);
            }
            catch (Exception)
            {
                await UpdateMessageWhenFailed(result, new NetworkException(ResponseCode.CONNECTIONERROR));
            }
            return result;
        }

        private void SetFileContentHeader(HttpContent fileContent, string fileName, string contentType)
        {
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "\"files\"",
                FileName = "\"" + fileName + "\""
            };
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
        }

        private async Task CheckIfResponseSuccess(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new NetworkException(ResponseCode.UNAUTHORISE);
                }
                else if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    throw new NetworkException(ResponseCode.NOPERMISSION);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new NetworkException(ResponseCode.APINOTFOUND);
                }

                throw new NetworkException(RemoveHTMLTags(content), ResponseCode.SERVERERROR);
            }
        }

        public async Task<TResult> HandleResponse<TResult>(HttpResponseMessage preloadedJson)
        {
            try
            {
                await CheckIfResponseSuccess(preloadedJson);

                if (preloadedJson != null && (preloadedJson.StatusCode == HttpStatusCode.OK || preloadedJson.StatusCode == HttpStatusCode.Created) && preloadedJson.IsSuccessStatusCode)
                {

                    var serialized = await preloadedJson.Content.ReadAsStringAsync();
                    var type = typeof(TResult);
                    if (type == typeof(string))
                    {
                        TypeConverter converter = TypeDescriptor.GetConverter(type);
                        if (converter.CanConvertFrom(typeof(string)))
                        {
                            return (TResult)converter.ConvertFrom(serialized);
                        }
                    }
                    TResult result = await Task.Run(() =>
                   JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));
                    return result;
                }
            }
            catch (NetworkException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new NetworkException(RemoveHTMLTags(ex.Message), ResponseCode.ERROR);
            }
            return default(TResult);
        }

        public TResult DeserializeObjectAsync<TResult>(string preloadedJson)
        {
            TResult result = JsonConvert.DeserializeObject<TResult>(preloadedJson, _serializerSettings);
            return result;
        }

        public async Task UpdateUnleashMessageWhenFailed(MessageResponse result, NetworkException e)
        {
            result.Code = e.ResponseCode;
            if (result.Code == ResponseCode.SERVERERROR)
            {
                var modelMessage = await JsonHelper.FromString<ModelUnleashMessageResponse>(e.Message);
                if (modelMessage != null)
                {
                    if (modelMessage.Description != null)
                    {
                        result.Messages = new List<string>() { modelMessage.Description };
                        result.Error = RemoveHTMLTags(modelMessage.Description);
                        result.ErrorDescription = RemoveHTMLTags(modelMessage.Description);
                    }
                    else
                    {
                        if (e.Message != null)
                        {
                            result.ErrorDescription = e.Message;
                        }
                    }

                }


            }
            else
            {
                result.ErrorDescription = string.Empty;
            }
        }

        public async Task UpdateMessageWhenFailed(MessageResponse result, NetworkException e)
        {
            result.Code = e.ResponseCode;
            if (result.Code == ResponseCode.SERVERERROR)
            {
                var modelMessage = await JsonHelper.FromString<ModelMessageResponse>(e.Message);
                if (modelMessage != null)
                {
                    result.Messages = modelMessage.ModelState?.Error;
                    result.Error = RemoveHTMLTags(modelMessage.Error);

                    result.ErrorDescription = modelMessage.ErrorDescription;

                    if (string.IsNullOrEmpty(result.ErrorDescription))
                    {
                        result.ErrorDescription = result.Error;
                    }

                    if (string.IsNullOrEmpty(result.ErrorDescription))
                    {
                        if (modelMessage.ModelState != null && modelMessage.ModelState.Error != null && modelMessage.ModelState.Error.Count > 0)
                        {
                            result.ErrorDescription = string.Join(",", modelMessage.ModelState.Error);
                        }
                    }
                }

                if (string.IsNullOrEmpty(result.ErrorDescription))
                {
                    var exceptionMessage = await JsonHelper.FromString<ExceptionMessageResponse>(e.Message);
                    if (exceptionMessage != null && !string.IsNullOrEmpty(exceptionMessage.Message))
                        result.ErrorDescription = exceptionMessage.Message;
                    else
                        result.ErrorDescription = string.Empty;
                }
            }
            else
            {
                result.ErrorDescription = string.Empty;
            }
        }

        private Dictionary<string, string> DictionaryFromType<T>(T data) where T : class
        {
            if (data == null) return new Dictionary<string, string>();
            Type t = data.GetType();
            PropertyInfo[] props = t.GetProperties();
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (PropertyInfo prp in props)
            {
                object value = prp.GetValue(data, new object[] { });
                var proName = prp.GetCustomAttribute<JsonPropertyAttribute>().PropertyName ?? prp.Name;
                if (value != null)
                    dict.Add(proName, value.ToString());
            }
            return dict;
        }

        public async Task<ResponseResult<TResult>> SendRequestWithHandleErrorAsync<TResult>(HttpRequestMessage requestMessage, string requestUri, string query = "")
        {
            ResponseResult<TResult> result = new ResponseResult<TResult>();
            result.Message = new MessageResponse();
            try
            {
                result.Result = await SendRequestAsync<TResult>(requestMessage, requestUri, query);
                result.Message.Code = ResponseCode.SUCCESS;
            }
            catch (NetworkException ex)
            {
                await UpdateMessageWhenFailed(result.Message, ex);
            }
            catch (Exception)
            {
                await UpdateMessageWhenFailed(result.Message, new NetworkException(ResponseCode.CONNECTIONERROR));
            }
            return result;
        }

        public async Task<ResponseResult<TResult>> SendRequestUnleashWithHandleErrorAsync<TResult>(HttpRequestMessage requestMessage, string requestUri, string query = "")
        {
            ResponseResult<TResult> result = new ResponseResult<TResult>();
            result.Message = new MessageResponse();
            try
            {
                result.Result = await SendRequestAsync<TResult>(requestMessage, requestUri, query);
                result.Message.Code = ResponseCode.SUCCESS;
            }
            catch (NetworkException ex)
            {
                await UpdateUnleashMessageWhenFailed(result.Message, ex);
            }
            catch (Exception)
            {
                await UpdateUnleashMessageWhenFailed(result.Message, new NetworkException(ResponseCode.CONNECTIONERROR));
            }
            return result;
        }

        public async Task<string> UploadStringWithHandleErrorAsync<TResult>(string method, string requestUri, string query = "", string postData = "")
        {
            if (httpClient == null)
                CreateHttpClient();

            var uri = new Uri(string.Format("{0}{1}{2}", httpClient.BaseAddress, requestUri, query));
            using (var client = new WebClient())
            {
                client.Headers.Add("api-auth-id", UnleasedAuthId);
                client.Headers.Add("api-auth-signature", GetUnleashedApiSignature((query.StartsWith("?") ? query.Trim('?') : ""), UnleasedPrivateKey));
                client.Headers.Add("Accept", "application/json");
                client.Headers.Add("Content-Type", "application/json; charset=" + client.Encoding.WebName);
                string response = string.Empty;
                try
                {
                    response = await client.UploadStringTaskAsync(uri, method, postData);
                }
                catch (WebException ex)
                {
                    var stream = ex.Response.GetResponseStream();
                    if (stream != null)
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            response = reader.ReadToEnd();
                        }
                    }
                }
                return RemoveHTMLTags(response);
            }
        }

        public async Task<ResponseResult<TResult>> GetRequestWithHandleErrorAsync<TResult>(string requestUri)
        {
            ResponseResult<TResult> result = new ResponseResult<TResult>();
            result.Message = new MessageResponse();
            try
            {
                result.Result = await GetRequestAsync<TResult>(requestUri);
                result.Message.Code = ResponseCode.SUCCESS;
            }
            catch (NetworkException ex)
            {
                await UpdateMessageWhenFailed(result.Message, ex);
            }
            catch (Exception)
            {
                await UpdateMessageWhenFailed(result.Message, new NetworkException(ResponseCode.CONNECTIONERROR));
            }
            return result;
        }

        public async Task<MessageResponse> GetRequestWithoutResultAsync(string requestUri)
        {
            var result = new MessageResponse();
            try
            {
                if (!string.IsNullOrEmpty(AccessToken))
                {
                    this.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
                }

                using (var requestResponse = await this.HttpClient.GetAsync(requestUri))
                    await CheckIfResponseSuccess(requestResponse);
                result.Code = ResponseCode.SUCCESS;
            }
            catch (NetworkException ex)
            {
                await UpdateMessageWhenFailed(result, ex);
            }
            catch (Exception)
            {
                await UpdateMessageWhenFailed(result, new NetworkException(ResponseCode.CONNECTIONERROR));
            }
            return result;
        }

        public async Task<ResponseResult<TResult>> PostRequestWithHandleErrorAsync<T, TResult>(string requestUri, T content)
        {
            ResponseResult<TResult> result = new ResponseResult<TResult>();
            result.Message = new MessageResponse();
            try
            {
                result.Result = await PostRequestAsync<T, TResult>(requestUri, content);
                result.Message.Code = ResponseCode.SUCCESS;
            }
            catch (NetworkException ex)
            {
                await UpdateMessageWhenFailed(result.Message, ex);
            }
            catch (Exception)
            {
                await UpdateMessageWhenFailed(result.Message, new NetworkException(ResponseCode.CONNECTIONERROR));
            }
            return result;
        }

        public async Task<MessageResponse> PostRequestWithHandleErrorAsyncWithoutResult<T>(string requestUri, T content)
        {
            var message = new MessageResponse();
            try
            {
                var jsonString = JsonConvert.SerializeObject(content);

                var stringJsonContent = new StringContent(jsonString, Encoding.UTF8, DataFormat);
                using (var requestResponse = await this.HttpClient.PostAsync(requestUri, stringJsonContent)) { }
                message.Code = ResponseCode.SUCCESS;
            }
            catch (NetworkException ex)
            {
                await UpdateMessageWhenFailed(message, ex);
            }
            catch (Exception)
            {
                await UpdateMessageWhenFailed(message, new NetworkException(ResponseCode.CONNECTIONERROR));
            }
            return message;
        }

        public async Task<MessageResponse> PostRequestWithoutResultAsync<T>(string requestUri, T content)
        {
            var result = new MessageResponse();
            try
            {
                if (!string.IsNullOrEmpty(AccessToken))
                {
                    this.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
                }

                var jsonString = JsonConvert.SerializeObject(content);
                var stringJsonContent = new StringContent(jsonString, Encoding.UTF8, DataFormat);

                using (var requestResponse = await this.HttpClient.PostAsync(requestUri, stringJsonContent))
                {
                    await CheckIfResponseSuccess(requestResponse);
                }
                result.Code = ResponseCode.SUCCESS;
            }
            catch (NetworkException ex)
            {
                await UpdateMessageWhenFailed(result, ex);
            }
            catch (Exception)
            {
                await UpdateMessageWhenFailed(result, new NetworkException(ResponseCode.CONNECTIONERROR));
            }
            return result;
        }

        public async Task<MessageResponse> PostRequestWithoutResultWithHandleErrorAsync(string requestUri)
        {
            var result = new MessageResponse();
            try
            {
                if (!string.IsNullOrEmpty(AccessToken))
                {
                    this.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
                }

                using (var requestResponse = await this.HttpClient.PostAsync(requestUri, null))
                    await CheckIfResponseSuccess(requestResponse);
                result.Code = ResponseCode.SUCCESS;
            }
            catch (NetworkException ex)
            {
                await UpdateMessageWhenFailed(result, ex);
            }
            catch (Exception)
            {
                await UpdateMessageWhenFailed(result, new NetworkException(ResponseCode.CONNECTIONERROR));
            }
            return result;
        }

        public async Task<MessageResponse> DeleteRequestWithHandleErrorWithoutResultAsync(string requestUri)
        {
            var result = new MessageResponse();
            try
            {
                if (!string.IsNullOrEmpty(AccessToken))
                {
                    this.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
                }

                using (var requestResponse = await this.HttpClient.DeleteAsync(requestUri))
                    await CheckIfResponseSuccess(requestResponse);
                result.Code = ResponseCode.SUCCESS;
            }
            catch (NetworkException ex)
            {
                await UpdateMessageWhenFailed(result, ex);
            }
            catch (Exception)
            {
                await UpdateMessageWhenFailed(result, new NetworkException(ResponseCode.CONNECTIONERROR));
            }
            return result;
        }

        public async Task<ResponseResult<TResult>> PostRequestWithHandleErrorAsync<TResult>(string requestUri)
        {
            ResponseResult<TResult> result = new ResponseResult<TResult>();
            result.Message = new MessageResponse();
            try
            {
                result.Result = await PostRequestAsync<TResult>(requestUri);
                result.Message.Code = ResponseCode.SUCCESS;
            }
            catch (NetworkException ex)
            {
                await UpdateMessageWhenFailed(result.Message, ex);
            }
            catch (Exception)
            {
                await UpdateMessageWhenFailed(result.Message, new NetworkException(ResponseCode.CONNECTIONERROR));
            }
            return result;
        }

        public void Dispose()
        {
            if (httpClient != null)
                httpClient.Dispose();
        }

        public bool IsEarlierVersion(string version)
        {
            return version.CompareTo(CurrentApiVersion) > 0;
        }

        public bool IsEarlier31Version()
        {
            return IsEarlierVersion("3.1");
        }

        public void UpdateUnleashKey()
        {
            UnleasedAuthId = Settings.EnpointUnleashedAuthIdSetting;
            UnleasedPrivateKey = Settings.EnpointUnleashedPrivateKeySetting;
        }

        public static string RemoveHTMLTags(string input)
        {
            if (input == null)
                return string.Empty;
            return Regex.Replace(input, "<.*?>", String.Empty);
        }
    }

}

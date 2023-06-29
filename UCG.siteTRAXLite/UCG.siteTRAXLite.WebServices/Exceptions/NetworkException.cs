using UCG.siteTRAXLite.DataContracts;

namespace UCG.siteTRAXLite.WebServices.Exceptions
{
    public class NetworkException : Exception
    {
        public NetworkException() { }
        public NetworkException(string message) : base(message) { }
        public NetworkException(ResponseCode responseCode) : base(string.Empty) { ResponseCode = responseCode; }
        public NetworkException(string message, ResponseCode responseCode) : base(message) { ResponseCode = responseCode; }

        public ResponseCode ResponseCode { get; set; }
    }
}

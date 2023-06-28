namespace UCG.siteTRAXLite.DataContracts
{
    public class MessageResponse
    {
        public ResponseCode Code { get; set; }
        public List<string> Messages { get; set; }
        public string Message
        {
            get
            {
                if (Messages != null && Messages.Count > 0)
                {
                    return string.Join(Environment.NewLine, Messages);
                }
                return string.Empty;
            }
        }

        public bool IsSuccess { get { return Code == ResponseCode.SUCCESS; } }

        public string Error { get; set; }
        public string ErrorDescription { get; set; }
    }
}

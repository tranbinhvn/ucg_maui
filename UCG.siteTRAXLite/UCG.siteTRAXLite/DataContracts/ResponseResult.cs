namespace UCG.siteTRAXLite.DataContracts
{
    public class ResponseResult<T>
    {
        public T Result { get; set; }
        public MessageResponse Message { get; set; }
    }
}

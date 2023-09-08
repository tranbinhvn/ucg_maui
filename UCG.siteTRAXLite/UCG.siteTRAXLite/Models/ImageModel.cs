namespace UCG.siteTRAXLite.Models
{
    public class ImageModel
    {
        public string FileName { get; set; }
        public string ImageUrl { get; set; }
        public ImageSource ImageSource { get; set; }
        public string ImageSourceStr { get; set; }
        public string ContentType { get; set; }

        public long FileSize { get; set; }

        private byte[] _Content;
        public byte[] GetContent() { return _Content; }
        public void SetContent(byte[] content) { _Content = content; }
        public ImageModel() { }

        public ImageModel(byte[] content)
        {
            _Content = content;
        }
    }
}

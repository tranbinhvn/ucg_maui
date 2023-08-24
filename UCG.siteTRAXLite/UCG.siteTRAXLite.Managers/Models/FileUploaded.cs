using UCG.siteTRAXLite.Common.Utils;

namespace UCG.siteTRAXLite.Managers.Models
{
    public class FileUploaded
    {
        public string FileName { get; set; }
        public byte[] Content { get; set; }
        public string FilePath { get; set; }

        public string MineType
        {
            get
            {
                var extension = Path.GetExtension(FileName);
                return MimeTypeMap.GetMimeType(extension);
            }
        }
    }
}

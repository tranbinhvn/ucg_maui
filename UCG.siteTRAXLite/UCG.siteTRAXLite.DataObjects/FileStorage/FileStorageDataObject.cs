using UCG.siteTRAXLite.DataObjects.DataObject;

namespace UCG.siteTRAXLite.DataObjects.FileStorage
{
    public class FileStorageDataObject : DataObjectBase<Guid>
    {
        public Guid ActionFK { get; set; }
        public string FileName { get; set; }
        public string AzureContainer { get; set; }
        public string AzureFolder { get; set; }
        public string LocalFilePath { get; set; }
    }
}

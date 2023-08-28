namespace UCG.siteTRAXLite.Entities
{
    public class FileStorageEntity
    {
        public Guid FileStorageK { get; set; }
        public string FileName { get; set; }
        public string AzureContainer { get; set; }
        public string AzureFolder { get; set; }
        public string LocalFilePath { get; set; }
    }
}

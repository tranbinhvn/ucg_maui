namespace UCG.siteTRAXLite.Common.Constants
{
    public static class AzureConstants
    {
        private static Dictionary<string, string> configurations = null;
        public static Dictionary<string, string> Configurations
        {
            get
            {
                if (configurations == null)
                {
                    configurations = new Dictionary<string, string>
                    {
                        { "AzureStorageContainer", "file-abc-folder" },
                        { "AzureStorageContainer:SiteAttachments", "sitetraxliteattachments" },
                        { "AzureStorageContainer:Thumbnails" , "thumbnails"},
                        { "AzureStorageConnectionString", "DefaultEndpointsProtocol=https;AccountName=sitetraxnonprod;AccountKey=d36imiFsu8GYB4nQRh9UuJOaIqDFkKOKk5hA9S+lZ0zc9KkiaPqDB5GQbU7I+Xaa6RElAlUXY/Oj1ebvL/WdrA==;EndpointSuffix=core.windows.net" },
                        { "AzureStorageAccountName" , "sitetraxnonprod" },
                        { "AzureStorageAccountKey", "d36imiFsu8GYB4nQRh9UuJOaIqDFkKOKk5hA9S+lZ0zc9KkiaPqDB5GQbU7I+Xaa6RElAlUXY/Oj1ebvL/WdrA=="}
                    };

                }

                return configurations;
            }
        }

        public const bool AzureBlobEnabledAll = true;
        public const int AzureStorageExpirDays = 90;
    }
}

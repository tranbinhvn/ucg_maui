using UCG.siteTRAXLite.Common.Constants;
using UCG.siteTRAXLite.Entities;
using UCG.siteTRAXLite.Entities.SorEforms;
using UCG.siteTRAXLite.Managers.Models;
using UCG.siteTRAXLite.WebServices.UploadService;

namespace UCG.siteTRAXLite.Managers
{
    public class UploadManager : IUploadManager
    {
        private readonly IUploadService _uploadService;

        public UploadManager(IUploadService uploadService) 
        { 
            _uploadService = uploadService;
        }

        public async Task<bool> UploadFileToAzureAsync(IList<FileUploaded> files, QuestionAttachmentEntity entity, bool isConnected = true)
        {
            var azureBlobEnabledAll = AzureConstants.AzureBlobEnabledAll;
            var azureContainer = AzureConstants.Configurations["AzureStorageContainer"] != null ? AzureConstants.Configurations["AzureStorageContainer"].ToString() : string.Empty;
            var azureFolder = AzureConstants.Configurations["AzureStorageContainer:SiteAttachments"] != null ? AzureConstants.Configurations["AzureStorageContainer:SiteAttachments"].ToString() : string.Empty;
            var attachmentFolder = string.Format(@"{0}", AzureConstants.Configurations["AzureStorageContainer:SiteAttachments"]);
            if (azureBlobEnabledAll)
            {
                if (!string.IsNullOrEmpty(azureContainer) && !string.IsNullOrEmpty(azureFolder))
                {
                    foreach (var file in files)
                    {
                        var newFile = new FileStorageEntity
                        {
                            FileStorageK = Guid.NewGuid(),
                            FileName = file.FileName,
                            AzureFolder = azureFolder
                        };

                        await _uploadService.UploadFileToAzureAsync(newFile, entity, attachmentFolder);
                        return true;
                    }
                }
            }

            return false;
        }
    }
}

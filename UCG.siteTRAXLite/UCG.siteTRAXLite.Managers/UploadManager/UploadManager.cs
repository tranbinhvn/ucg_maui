using Microsoft.Maui.Networking;
using UCG.siteTRAXLite.Common.Constants;
using UCG.siteTRAXLite.DataObjects.FileStorage;
using UCG.siteTRAXLite.Entities;
using UCG.siteTRAXLite.Entities.SorEforms;
using UCG.siteTRAXLite.Managers.Mappers;
using UCG.siteTRAXLite.Managers.Models;
using UCG.siteTRAXLite.Repositories.FileStorage;
using UCG.siteTRAXLite.WebServices.UploadService;

namespace UCG.siteTRAXLite.Managers
{
    public class UploadManager : ManagerBase, IUploadManager
    {
        private readonly IUploadService _uploadService;
        private readonly IFileStorageRepository _fileRepo;

        public UploadManager(IConnectivity connectivity,
            IServiceEntityMapper mapper,
            IUploadService uploadService,
            IFileStorageRepository fileRepo) : base(connectivity, mapper)
        {
            _uploadService = uploadService;
            _fileRepo = fileRepo;
        }

        public async Task<bool> UploadFileToAzureAsync(IList<FileUploaded> files, QuestionAttachmentEntity entity, bool isConnected = true, Guid? actionFK = null)
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
                            AzureContainer = azureContainer,
                            AzureFolder = azureFolder
                        };

                        if (actionFK != null)
                        {
                            newFile.ActionFK = actionFK.Value;
                            var fileObj = Mapper.Map<FileStorageDataObject>(newFile);
                            _fileRepo.Save(fileObj);
                        }

                        await _uploadService.UploadFileToAzureAsync(newFile, entity, attachmentFolder);
                        return true;
                    }
                }
            }

            return false;
        }
    }
}

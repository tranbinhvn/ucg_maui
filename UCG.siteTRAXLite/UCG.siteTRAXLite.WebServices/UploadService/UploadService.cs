using UCG.siteTRAXLite.Entities;
using UCG.siteTRAXLite.Entities.SorEforms;
using UCG.siteTRAXLite.WebServices.AzureBlob;

namespace UCG.siteTRAXLite.WebServices.UploadService
{
    public class UploadService : IUploadService
    {
        private readonly IAzureBlobService _azureBlobService;

        public UploadService(IAzureBlobService azureBlobService) 
        { 
            _azureBlobService = azureBlobService;
        }

        public async Task UploadFileToAzureAsync(FileStorageEntity fileStorage, QuestionAttachmentEntity file, string storagePath)
        {
            file.IsUploading = true;

            var progressHandler = new Progress<long>((progress) =>
            {
                file.Progress = file.FileSize != 0 ? (double)progress / file.FileSize : 1;
                file.TextProgress = $"{file.Progress * 100}%";
            });

            await _azureBlobService.UploadFileAsync(fileStorage, file.Source, file.ContentType.ToString(), progressHandler);

            file.IsComplete = true;
        }
    }
}

using UCG.siteTRAXLite.Entities;
using UCG.siteTRAXLite.Entities.SorEforms;

namespace UCG.siteTRAXLite.WebServices.UploadService
{
    public interface IUploadService
    {
        Task UploadFileToAzureAsync(FileStorageEntity fileStorage, QuestionAttachmentEntity file, string storagePath);
    }
}

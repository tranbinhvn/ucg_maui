using UCG.siteTRAXLite.Entities.SorEforms;
using UCG.siteTRAXLite.Managers.Models;

namespace UCG.siteTRAXLite.Managers
{
    public interface IUploadManager
    {
        Task<bool> UploadFileToAzureAsync(IList<FileUploaded> files, QuestionAttachmentEntity fileData , bool isConnected = true, Guid? actionFK = null);
    }
}

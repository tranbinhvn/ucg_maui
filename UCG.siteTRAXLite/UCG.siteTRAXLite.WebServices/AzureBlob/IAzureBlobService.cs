using Azure.Storage.Blobs.Specialized;
using UCG.siteTRAXLite.Entities;

namespace UCG.siteTRAXLite.WebServices.AzureBlob
{
    public interface IAzureBlobService
    {
        Task<BlockBlobClient> GetBlockBlobReferenceAsync(string azureContainer, string azureFolder, string fileName);
        BlockBlobClient GetBlockBlobReference(string azureContainer, string azureFolder, string fileName);
        Task UploadFileAsync(FileStorageEntity fileStorage, string src, string contentType, IProgress<long> progressHandler = null);
        Task<bool> BlobExistsOnCloudAsync(string azureContainer, string azureFolder, string fileName);
        bool BlobExistsOnCloud(string azureContainer, string azureFolder, string fileName);
    }
}

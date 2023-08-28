using Azure.Storage.Blobs;

namespace UCG.siteTRAXLite.WebServices.AzureBlob
{
    public interface IAzureBlobConnectionFactory
    {
        BlobContainerClient GetBlobContainer(string containerName);
        Task<BlobContainerClient> GetBlobContainerAsync(string containerName);
    }
}

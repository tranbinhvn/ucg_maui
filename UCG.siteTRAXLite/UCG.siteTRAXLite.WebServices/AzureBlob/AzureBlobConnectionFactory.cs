using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using UCG.siteTRAXLite.Common.Constants;

namespace UCG.siteTRAXLite.WebServices.AzureBlob
{
    public class AzureBlobConnectionFactory : IAzureBlobConnectionFactory
    {
        private BlobServiceClient _blobServiceClient;
        private BlobContainerClient _blobContainerClient;

        public BlobContainerClient GetBlobContainer(string containerName)
        {
            if (_blobContainerClient != null)
                return _blobContainerClient;

            if (string.IsNullOrWhiteSpace(containerName))
            {
                throw new ArgumentException("Configuration must contain ContainerName");
            }

            var blobServiceClient = GetServiceClient();

            _blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
            _blobContainerClient.CreateIfNotExists(PublicAccessType.Blob);
            return _blobContainerClient;
        }

        public async Task<BlobContainerClient> GetBlobContainerAsync(string containerName)
        {
            if (_blobContainerClient != null)
                return _blobContainerClient;

            if (string.IsNullOrWhiteSpace(containerName))
            {
                throw new ArgumentException("Configuration must contain ContainerName");
            }

            var blobServiceClient = GetServiceClient();

            _blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
            await _blobContainerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);
            return _blobContainerClient;
        }

        private BlobServiceClient GetServiceClient()
        {
            if (_blobServiceClient != null)
                return _blobServiceClient;

            string connectionString = AzureConstants.Configurations["AzureStorageConnectionString"];
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("Configuration must contain StorageConnectionString");
            }

            _blobServiceClient = new BlobServiceClient(connectionString);
            return _blobServiceClient;
        }
    }
}

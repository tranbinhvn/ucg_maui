using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using SkiaSharp;
using UCG.siteTRAXLite.Common.Constants;
using UCG.siteTRAXLite.Entities;

namespace UCG.siteTRAXLite.WebServices.AzureBlob
{
    public class AzureBlobService : IAzureBlobService
    {
        private readonly IAzureBlobConnectionFactory _azureBlobConnectionFactory;

        public AzureBlobService(IAzureBlobConnectionFactory azureBlobConnectionFactory)
        {
            _azureBlobConnectionFactory = azureBlobConnectionFactory;
        }

        public async Task<BlockBlobClient> GetBlockBlobReferenceAsync(string azureContainer, string azureFolder, string fileName)
        {
            var blobContainer = await _azureBlobConnectionFactory.GetBlobContainerAsync(azureContainer);
            if (!string.IsNullOrEmpty(azureFolder))
            {
                return blobContainer.GetBlockBlobClient(Path.Combine(azureFolder, fileName));
            }
            else
            {
                return blobContainer.GetBlockBlobClient(fileName);
            }
        }

        public BlockBlobClient GetBlockBlobReference(string azureContainer, string azureFolder, string fileName)
        {
            var blobContainer = _azureBlobConnectionFactory.GetBlobContainer(azureContainer);
            if(!string.IsNullOrEmpty(azureFolder))
            {
                return blobContainer.GetBlockBlobClient(Path.Combine(azureFolder, fileName));
            }
            else
            {
                return blobContainer.GetBlockBlobClient(fileName);
            }
        }

        public async Task UploadFileAsync(FileStorageEntity fileStorage, string src, string contentType, IProgress<long> progressHandler = null)
        {
            try
            {
                fileStorage.AzureContainer = AzureConstants.Configurations["AzureStorageContainer"];
                var blobExistsOnCloud = await BlobExistsOnCloudAsync(fileStorage.AzureContainer, fileStorage.AzureFolder, fileStorage.FileName);

                if (blobExistsOnCloud)
                {
                    var timeStamp = DateTime.Now.ToUniversalTime().ToString("_yyyyMMddHHmmssffff");
                    fileStorage.FileName = Path.GetFileNameWithoutExtension(fileStorage.FileName) + timeStamp + Path.GetExtension(fileStorage.FileName);
                }

                var blob = await GetBlockBlobReferenceAsync(fileStorage.AzureContainer, fileStorage.AzureFolder, fileStorage.FileName);

                var blobUploadOptions = new BlobUploadOptions
                {
                    HttpHeaders = new BlobHttpHeaders
                    {
                        ContentType = contentType
                    },
                    ProgressHandler = progressHandler
                };

                await using var stream = File.OpenRead(src);
                await blob.UploadAsync(stream, blobUploadOptions);
                await stream.DisposeAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> BlobExistsOnCloudAsync(string azureContainer, string azureFolder, string fileName)
        {
            var blob = await GetBlockBlobReferenceAsync(azureContainer, azureFolder, fileName);
            return await blob.ExistsAsync();
        }

        public bool BlobExistsOnCloud(string azureContainer, string azureFolder, string fileName)
        {
            var blob = GetBlockBlobReference(azureContainer, azureFolder, fileName);
            return blob.Exists();
        }

        public async Task GenerateThumbnailImageAzureAsync(FileStorageEntity fileStorage, string source, string contentType)
        {
            string thumbnailsPath = AzureConstants.Configurations["AzureStorageContainer:Thumbnails"];
            var blob = await GetBlockBlobReferenceAsync(fileStorage.AzureContainer, thumbnailsPath, fileStorage.FileName);
            var blobUploadOptions = new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders
                {
                    ContentType = contentType
                }
            };

            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (SKBitmap skBitmap = SKBitmap.Decode(source))
                    {
                        int maxWidth = 480;
                        int maxHeight = 360;
                        int newWidth;
                        int newHeight;

                        if (skBitmap.Width > maxWidth || skBitmap.Height > maxHeight)
                        {
                            float ratioX = (float)maxWidth / skBitmap.Width;
                            float ratioY = (float)maxHeight / skBitmap.Height;
                            float ratio = Math.Min(ratioX, ratioY);

                            newWidth = (int)(skBitmap.Width * ratio);
                            newHeight = (int)(skBitmap.Height * ratio);
                        }
                        else
                        {
                            newWidth = skBitmap.Width;
                            newHeight = skBitmap.Height;
                        }

                        using (SKBitmap resizedBitmap = skBitmap.Resize(new SKImageInfo(newWidth, newHeight), SKFilterQuality.High))
                        using (SKImage resizedImage = SKImage.FromBitmap(resizedBitmap))
                        {
                            using (SKData encodedData = resizedImage.Encode(SKEncodedImageFormat.Jpeg, 90))
                            {
                                encodedData.SaveTo(memoryStream);
                                memoryStream.Position = 0;
                                await blob.UploadAsync(memoryStream, blobUploadOptions);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

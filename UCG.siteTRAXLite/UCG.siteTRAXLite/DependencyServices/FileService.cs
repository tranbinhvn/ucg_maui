using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using SkiaSharp;
using UCG.siteTRAXLite.Common.Utils;
using UCG.siteTRAXLite.Messages;
using UCG.siteTRAXLite.Models;

namespace UCG.siteTRAXLite.DependencyServices
{
    public class FileService : IFileService
    {
        public Task<bool> CoppyFileToDownLoadFolder(string tempPath)
        {
            throw new NotImplementedException();
        }

        public string GetDownloadFolder()
        {
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            return documents;
        }

        public void OpenFile(string filePath)
        {
            try
            {
                Launcher.OpenAsync(new OpenFileRequest
                {
                    File = new ReadOnlyFile(filePath)
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ImageModel>> SelectMultiFile()
        {
            try
            {
                var options = new PickOptions
                {
                    PickerTitle = "Select Files",
                };

                var pickedFiles = await FilePicker.PickMultipleAsync(options);
                if (pickedFiles != null)
                {
                    List<ImageModel> images = new List<ImageModel>();
                    foreach (var file in pickedFiles)
                    {
                        var imageModel = new ImageModel();
                        imageModel.ImageUrl = file.FullPath;

                        string filename = file.FileName;
                        string pathname = file.FullPath;

                        imageModel.FileName = filename;
                        imageModel.ContentType = file.ContentType;


                        if (filename == null && pathname != null)
                        {
                            filename = Path.GetFileName(pathname);
                        }

                        using var stream = await file.OpenReadAsync();
                        byte[] datas = FileUtils.GetBytesFromStream(stream);

                        imageModel.ImageUrl = await FileUtils.WriteToTempPath(datas, Path.GetFileName(pathname));
                        imageModel.FileSize = new FileInfo(imageModel.ImageUrl).Length;

                        images.Add(imageModel);
                    }

                    return images;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public byte[] ResizeImage(byte[] imageData, float width, float height, int quality)
        {
            using (SKBitmap originalImage = SKBitmap.Decode(imageData))
            {
                float oldWidth = originalImage.Width;
                float oldHeight = originalImage.Height;
                float scaleFactor = 0f;

                if (oldWidth > oldHeight)
                {
                    scaleFactor = width / oldWidth;
                }
                else
                {
                    scaleFactor = height / oldHeight;
                }

                int newHeight = (int)(oldHeight * scaleFactor);
                int newWidth = (int)(oldWidth * scaleFactor);

                using (SKBitmap resizedImage = originalImage.Resize(new SKImageInfo(newWidth, newHeight), SKFilterQuality.High))
                {
                    using (SKImage image = SKImage.FromBitmap(resizedImage))
                    {
                        using (SKData encoded = image.Encode(SKEncodedImageFormat.Jpeg, quality))
                        {
                            using (MemoryStream ms = new MemoryStream(encoded.ToArray()))
                            {
                                return ms.ToArray();
                            }
                        }
                    }
                }
            }
        }

        public Task SaveFile(string fileName, byte[] content, string mimetype)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveFileToDownLoadFolder(string tempPath)
        {
            throw new NotImplementedException();
        }
    }
}

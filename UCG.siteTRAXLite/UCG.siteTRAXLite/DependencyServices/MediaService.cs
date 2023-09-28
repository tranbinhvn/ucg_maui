using SkiaSharp;
using UCG.siteTRAXLite.Common.Utils;
using UCG.siteTRAXLite.Models;

namespace UCG.siteTRAXLite.DependencyServices
{
    public class MediaService : IMediaService
    {
        public async Task<ImageModel> OpenGallery()
        {
            try
            {
                var file = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Select Images",
                });

                if (file == null)
                    return null;

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

                return imageModel;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error picking photos: {ex}");
            }

            return null;
        }

        public async Task<ImageModel> TakePhoto()
        {
            try
            {
                var file = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = "Capture Photo",
                });

                if (file == null)
                    return null;

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

                return imageModel;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error picking photos: {ex}");
            }

            return null;
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
    }
}

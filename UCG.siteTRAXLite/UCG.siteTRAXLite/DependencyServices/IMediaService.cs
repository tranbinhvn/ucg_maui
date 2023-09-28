using UCG.siteTRAXLite.Models;

namespace UCG.siteTRAXLite.DependencyServices
{
    public interface IMediaService
    {
        Task<ImageModel> OpenGallery();
        Task<ImageModel> TakePhoto();
        byte[] ResizeImage(byte[] imageData, float width, float height, int quality);
    }
}

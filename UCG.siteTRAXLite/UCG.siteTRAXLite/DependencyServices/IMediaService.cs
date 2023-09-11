using UCG.siteTRAXLite.Models;

namespace UCG.siteTRAXLite.DependencyServices
{
    public interface IMediaService
    {
        Task<ImageModel> OpenGallery();
        byte[] ResizeImage(byte[] imageData, float width, float height, int quality);
    }
}

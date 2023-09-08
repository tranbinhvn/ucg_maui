using UCG.siteTRAXLite.Models;

namespace UCG.siteTRAXLite.DependencyServices
{
    public interface IFileService
    {
        Task<bool> SaveFileToDownLoadFolder(string tempPath);
        Task<bool> CoppyFileToDownLoadFolder(string tempPath);
        void OpenFile(string filePath);
        string GetDownloadFolder();
        Task<List<ImageModel>> SelectMultiFile();
        byte[] ResizeImage(byte[] imageData, float width, float height, int quality);
        Task SaveFile(string fileName, byte[] content, string mimetype);
    }
}

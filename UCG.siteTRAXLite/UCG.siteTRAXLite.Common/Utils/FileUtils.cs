using UCG.siteTRAXLite.Common.Extensions;

namespace UCG.siteTRAXLite.Common.Utils
{
    public class FileUtils
    {
        public static List<string> imageExtension = new List<string> { ".png", ".bmp", ".dib", ".jpg", ".jpeg", ".jpe", ".jfif", ".gif", ".tif", ".tiff", ".heif", ".heic" };
        public static List<string> documentExtension = new List<string> { ".docx", ".docm", ".doc", ".dotx", ".dotm", ".dot", ".pdf", ".xps", ".rtf", ".txt", ".xml", ".xls", ".xlsx", ".xlsm", ".odt", ".wps", ".csv", ".sql", ".vsdx", ".pptx", ".ppt", ".pptm", ".pptm", ".potx", ".potm", ".pot", ".ppsx", ".pps", ".odp", ".sor", ".msg", ".dwg", ".html", ".gml" };
        public static List<string> compressExtension = new List<string> { ".zip", ".rar", ".arj", ".tar", ".gz", ".tgz", ".7z" };
        public static bool IsImage(string extention)
        {
            return imageExtension.Contains(extention);
        }

        public static bool IsDocument(string extention)
        {
            return documentExtension.Contains(extention);
        }

        public static bool IsCompress(string extention)
        {
            return compressExtension.Contains(extention);
        }

        public static string GenerateUploadFileName()
        {
            return $"UploadFile_{DateTime.UtcNow.ToString("YYYYMMDDHHMMSS")}.jpg";
        }

        public static byte[] GetBytesFromStream(Stream input, bool closeStream = true)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                if (closeStream)
                    input.Dispose();
                return ms.ToArray();
            }
        }

        public static byte[] GetBytesFromFilePath(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }

        public static bool IsExist(string filePath)
        {
            return File.Exists(filePath);
        }

        public static double ByteToKB(long bytes)
        {
            return Math.Round((double)bytes / 1024, 2);
        }

        public static string GetFileName(string path)
        {
            return Path.GetFileName(path);
        }

        public static string GetServerUploadFile(List<string> serverResponse)
        {
            if (serverResponse != null && serverResponse.Count > 0)
            {
                var splitData = serverResponse[0].Split('\\');
                return splitData[splitData.Length - 1];
            }
            return string.Empty;
        }

        public static void RemoveFolders(string folder)
        {
            if (Directory.Exists(folder))
            {
                System.IO.DirectoryInfo di = new DirectoryInfo(folder);
                di.Delete(true);
            }
        }

        public static String GetTemFolder()
        {
            return System.IO.Path.GetTempPath();
        }


        public static string GetTempFolderForFile()
        {
            var tempFolder = GetTemFolder();

            var folder = Path.Combine(tempFolder, Guid.NewGuid().ToString());
            Directory.CreateDirectory(folder);
            return folder;
        }

        public async static Task<String> WriteToTempPath(byte[] data, String fileName)
        {
            var tempFolder = GetTemFolder();

            var folder = Path.Combine(tempFolder, Guid.NewGuid().ToString());
            Directory.CreateDirectory(folder);
            var filePath = Path.Combine(folder, fileName);

            using (var outputStream = File.OpenWrite(filePath))
            {
                await outputStream.WriteAsync(data, 0, data.Length);
                await outputStream.FlushAsync();
            }
            return filePath;
        }

        public static void DeleteFile(String filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                FileInfo f = new FileInfo(filePath);
                if (f.Exists)
                    f.Delete();
            }
        }

        public static long GetFileSize(string filePath)
        {
            return new System.IO.FileInfo(filePath).Length;
        }

        public static bool CheckIsImage(string fileName)
        {
            return MimeTypeMap.GetMimeType(Path.GetExtension(fileName)).StartsWithWithCulture("image");
        }

    }
}

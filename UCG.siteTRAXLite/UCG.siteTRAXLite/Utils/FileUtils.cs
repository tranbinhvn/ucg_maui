namespace UCG.siteTRAXLite.Utils
{
    public class FileUtils
    {
        public static double ByteToKB(long bytes)
        {
            return Math.Round((double)bytes / 1024, 2);
        }

        public static long GetFileSize(string filePath)
        {
            return new FileInfo(filePath).Length;
        }
    }
}

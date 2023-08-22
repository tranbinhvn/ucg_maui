using Acr.UserDialogs;
using UCG.siteTRAXLite.Common.Extensions;
using UCG.siteTRAXLite.Extensions;
using UCG.siteTRAXLite.WebServices.Helper;

namespace UCG.siteTRAXLite.Helpers
{
    public static class FileUploadHelper
    {
        public static bool ValidateExtention(List<string> files)
        {
            if (string.IsNullOrEmpty(Settings.FileExtensions))
                return true;
            var splitextension = Settings.FileExtensions.Split(',');
            var res = files.Where(f => !splitextension.Contains(Path.GetExtension(f).ToLowerWithCulture()));
            var extentionInvalid = string.Join(", ", res.Select(f => f));
            if (!string.IsNullOrEmpty(extentionInvalid))
            {
                //FuncEx.ExcuteAsync<string, string, string, CancellationToken?>(UserDialogs.Instance.AlertAsync, $"File extention of {extentionInvalid} is invalid", null, null, null);
                return false;
            }
            return true;
        }

        public static bool ValidateExtention(string fileName)
        {
            if (string.IsNullOrEmpty(Settings.FileExtensions))
                return true;
            var splitextension = Settings.FileExtensions.Split(',');
            var isValid = splitextension.Contains(Path.GetExtension(fileName).ToLowerWithCulture());
            if (!isValid)
            {
                //FuncEx.ExcuteAsync<string, string, string, CancellationToken?>(UserDialogs.Instance.AlertAsync, $"File extention of {fileName} is invalid", null, null, null);
                return false;
            }
            return true;
        }

        public static bool IsDuplicate(string file1Path, string file2Path)
        {
            return AreFilesEqual(file1Path, file2Path);
        }

        public static bool IsDuplicate(string filePath, List<string> fileList)
        {
            foreach (string file in fileList)
            {
                if (AreFilesEqual(filePath, file))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool AreFilesEqual(string filePath1, string filePath2)
        {
            using (var stream1 = new FileStream(filePath1, FileMode.Open, FileAccess.Read))
            using (var stream2 = new FileStream(filePath2, FileMode.Open, FileAccess.Read))
            using (var reader1 = new StreamReader(stream1))
            using (var reader2 = new StreamReader(stream2))
            {
                while (!reader1.EndOfStream && !reader2.EndOfStream)
                {
                    if (reader1.ReadLine() != reader2.ReadLine())
                    {
                        return false;
                    }
                }

                return reader1.EndOfStream && reader2.EndOfStream;
            }
        }
    }
}

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
    }
}

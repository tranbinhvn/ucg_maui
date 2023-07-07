using Microsoft.Maui.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCG.siteTRAXLite.WebServices.DependencyServices
{
    public class FileService : IFileService
    {
        public async Task<string> ReadFileFromRawsFolder(string folder, string fileName)
        {
            var jsonContent = "";
            using (Stream fileStream = await FileSystem.Current.OpenAppPackageFileAsync(Path.Combine(folder, fileName)))
            {
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    jsonContent = await reader.ReadToEndAsync();
                }
            }
            return jsonContent;
        }

    }
}

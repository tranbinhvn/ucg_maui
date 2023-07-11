using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCG.siteTRAXLite.WebServices.DependencyServices
{
    public interface IFileService
    {
        Task<string> ReadFileFromRawsFolder(string folder, string fileName);
    }
}

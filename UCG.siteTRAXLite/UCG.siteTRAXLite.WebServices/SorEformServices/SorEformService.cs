using Microsoft.Maui.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UCG.siteTRAXLite.Common.Constants;
using UCG.siteTRAXLite.DataContracts;
using UCG.siteTRAXLite.DataContracts.SorEformContracts;
using UCG.siteTRAXLite.Entities.SorEforms;
using UCG.siteTRAXLite.WebServices.DependencyServices;

namespace UCG.siteTRAXLite.WebServices.SorEformServices
{
    public class SorEformService : WebServiceBase, ISorEformService
    {
        private readonly IFileService _fileService;
        public SorEformService(IFileService fileService) : base(EndPointType.DPPEndpoint)
        {
            _fileService = fileService;
        }

        private async Task<SorEformConfigDTO> LoadSorEformConfig()
        {
            try
            {
                var jsonContent = await _fileService.ReadFileFromRawsFolder(MessageStrings.ConfigFolderName, MessageStrings.ConfigFileName);

                var config = JsonConvert.DeserializeObject<SorEformConfigDTO>(jsonContent);

                return config;

            } 
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ResponseResult<List<SectionDTO>>> GetSections()
        {
            var config = await LoadSorEformConfig();
            
            return CreateResponseResult(config.JobTab.Sections);
        }

        public async Task<ResponseResult<List<BreadcrumbDTO>>> GetGenericSectionBreadcrumbs()
        {
            var config = await LoadSorEformConfig();

            var sections = config?.JobTab?.Sections;

            var genericSection = sections?
                .Where(s => !string.IsNullOrEmpty(s.SectionType) && (s.SectionType).Equals("Generic", StringComparison.OrdinalIgnoreCase))?.FirstOrDefault();
            
            return CreateResponseResult(genericSection?.Breadcrumbs);
        }

        private ResponseResult<T> CreateResponseResult<T>(T result)
        {
            var response = new ResponseResult<T>
            {
                Result = result,
                Message = new MessageResponse
                {
                    Code = ResponseCode.SUCCESS
                }
            };
            return response;
        }
    }
}

using Microsoft.Maui.Storage;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UCG.siteTRAXLite.Common.Constants;
using UCG.siteTRAXLite.DataContracts;
using UCG.siteTRAXLite.DataContracts.SorEformContracts;
using UCG.siteTRAXLite.DataContracts.SorEformContracts.Sections;
using UCG.siteTRAXLite.Entities.SorEforms;
using UCG.siteTRAXLite.Entities.SorEforms.Sections;
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

                var config = JsonConvert.DeserializeObject<SorEformConfigDTO>(jsonContent, new ResponseDataConverter());

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

        public async Task<ResponseResult<List<StepperDTO>>> GetGenericSectionSteppers()
        {
            var config = await LoadSorEformConfig();

            var sections = config?.JobTab?.Sections;

            var genericSection = sections?
                .Where(s => !string.IsNullOrEmpty(s.SectionType) && (s.SectionType).Equals("Generic", StringComparison.OrdinalIgnoreCase))?.FirstOrDefault();
            
            return CreateResponseResult(genericSection?.Steppers);
        }

        public async Task<ResponseResult<Take5StepperDTO>> GetTake5Steppers()
        {
            try
            {
                var config = await LoadSorEformConfig();

                var take5Section = config?.JobTab?.Sections?.Where(s => !string.IsNullOrEmpty(s.SectionType) && (s.SectionType).Equals("take5", StringComparison.OrdinalIgnoreCase))?.FirstOrDefault();

                return CreateResponseResult(new Take5StepperDTO
                {
                    StepperControl = take5Section?.StepperControl,
                    StepperHazard = take5Section?.StepperHazard,
                    StepperSubmit = take5Section?.StepperSubmit,
                });

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ResponseResult<SorClaimsStepperDTO>> GetSorClaimsSectionSteppers()
        {
            try
            {
                var config = await LoadSorEformConfig();

                var sorClaimsSection = config?.JobTab?.Sections?.Where(s => !string.IsNullOrEmpty(s.SectionType) && (s.SectionType).Equals("sorclaims", StringComparison.OrdinalIgnoreCase))?.FirstOrDefault();

                return CreateResponseResult(new SorClaimsStepperDTO
                {
                    StepperControl = sorClaimsSection?.StepperControl,
                    StepperUploadFiles = sorClaimsSection?.StepperUploadFiles,
                    StepperSubmit = sorClaimsSection?.StepperSubmit,
                });

            }
            catch (Exception ex)
            {
                return null;
            }
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

    public class ResponseDataConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(List<ResponseDataItemDTO>);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);
            var responses = new List<ResponseDataItemDTO>();

            if (token.Type == JTokenType.Array)
            {
                foreach (var item in token)
                {
                    var response = new ResponseDataItemDTO();

                    if (item.Type == JTokenType.String)
                    {
                        response.Value = (string)item;
                        responses.Add(response);
                    }
                    else if (item.Type == JTokenType.Object)
                    {
                        if (item["name"] != null)
                        {
                            response.Value = item.Value<string>("name");
                        }
                        else if(item["title"] != null)
                        {
                            response.Value = item.Value<string>("title");
                        }
                        
                        if (item["responseData"] != null)
                        {
                            response.ResponseData = item["responseData"].ToObject<List<ResponseDataItemDTO>>(serializer);
                        }

                        if (item["actionList"] != null)
                        {
                            response.ActionList = item["actionList"].ToObject<List<ActionItemDTO>>(serializer);
                        }

                        responses.Add(response);
                    }

                }

                return responses;
            }
            
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}

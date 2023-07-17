﻿using Microsoft.Maui.Storage;
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

        private async Task<SorEformConfigDto> LoadSorEformConfig()
        {
            var jsonContent = await _fileService.ReadFileFromRawsFolder(MessageStrings.ConfigFolderName, MessageStrings.ConfigFileName);

            return JsonConvert.DeserializeObject<SorEformConfigDto>(jsonContent);
        }

        public async Task<ResponseResult<List<ActionItemDto>>> GetActionsByOutcome(string outcomeName)
        {
            var config = await LoadSorEformConfig();
            var outcome = config?.Settings?.OutcomeOptions?.FirstOrDefault(o => o.Name.Equals(outcomeName, StringComparison.OrdinalIgnoreCase));

            return CreateResponseResult(outcome.ActionList);
        }

        public async Task<ResponseResult<List<string>>> GetOutcomeNames()
        {
            var jsonContent = await _fileService.ReadFileFromRawsFolder(MessageStrings.ConfigFolderName, MessageStrings.ConfigFileName);
            var config = JsonConvert.DeserializeObject<SorEformConfigDto>(jsonContent);
            var outcomes = config?.Settings?.OutcomeOptions?.Select(a => a.Name)?.ToList();

            return CreateResponseResult(outcomes);
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
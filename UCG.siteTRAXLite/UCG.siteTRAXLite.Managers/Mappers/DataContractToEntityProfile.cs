using AutoMapper;
using UCG.siteTRAXLite.DataContracts;
using UCG.siteTRAXLite.DataContracts.SorEformContracts;
using UCG.siteTRAXLite.DataContracts.UserInfoContracts;
using UCG.siteTRAXLite.Entities;
using UCG.siteTRAXLite.Entities.SorEforms;

namespace UCG.siteTRAXLite.Managers.Mappers
{
    public class DataContractToEntityProfile : Profile
    {
        public DataContractToEntityProfile()
        {
            CreateMap<UserInfoDto, UserInfoEntity>(MemberList.None)
                .ReverseMap();
            CreateMap<SorEformConfigDto, SorEformConfigEntity>(MemberList.None)
                .ReverseMap();
            CreateMap<ConfigInfoDto, ConfigInfoEntity>(MemberList.None)
                .ReverseMap();
            CreateMap<SettingsDto, SettingsEntity>(MemberList.None)
                .ReverseMap();
            CreateMap<OutcomeOptionDto, OutcomeOptionEntity>(MemberList.None)
                .ReverseMap();
            CreateMap<ActionItemDto, ActionItemEntity>(MemberList.None)
                .ReverseMap();
            CreateMap<ConditionDto, ConditionEntity>(MemberList.None)
                .ReverseMap();
            CreateMap<LaunchDataDTO, LaunchDataEntity>(MemberList.None)
                .ReverseMap();
            CreateMap<JobDetailDTO, JobDetailEntity>(MemberList.None)
                .ReverseMap();
        }
    }
}

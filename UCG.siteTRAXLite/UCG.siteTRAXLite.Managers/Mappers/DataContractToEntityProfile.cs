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
            CreateMap<SorEformConfigDTO, SorEformConfigEntity>(MemberList.None)
                .ReverseMap();
            CreateMap<ConfigInfoDTO, ConfigInfoEntity>(MemberList.None)
                .ReverseMap();
            CreateMap<JobTabDTO, JobTabEntity>(MemberList.None)
                .ReverseMap();
            CreateMap<SectionDTO, SectionEntity>(MemberList.None)
                .ReverseMap();
            CreateMap<BreadcrumbDTO, BreadcrumbEntity>(MemberList.None)
                .ReverseMap();
            CreateMap<ActionItemDTO, ActionItemEntity>(MemberList.None)
                .ReverseMap();
            CreateMap<ConditionDTO, ConditionEntity>(MemberList.None)
                .ReverseMap();
            CreateMap<LaunchDataDTO, LaunchDataEntity>(MemberList.None)
                .ReverseMap();
            CreateMap<JobDetailDTO, JobDetailEntity>(MemberList.None)
                .ReverseMap();
            CreateMap<Take5BreadcrumbDTO, Take5BreadcrumbEntity>(MemberList.None)
                .ReverseMap();
            CreateMap<ResponseDataItemDTO, ResponseDataItemEntity>(MemberList.None)
                .ReverseMap();
        }
    }
}

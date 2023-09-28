using AutoMapper;
using UCG.siteTRAXLite.DataObjects;
using UCG.siteTRAXLite.DataObjects.Configuration;
using UCG.siteTRAXLite.DataObjects.FileStorage;
using UCG.siteTRAXLite.DataObjects.Job;
using UCG.siteTRAXLite.DataObjects.Site;
using UCG.siteTRAXLite.Entities;
using UCG.siteTRAXLite.Entities.Configuration;
using UCG.siteTRAXLite.Entities.Job;
using UCG.siteTRAXLite.Entities.Site;
using UCG.siteTRAXLite.Entities.SorEforms;

namespace UCG.siteTRAXLite.Managers.Mappers
{
    public class EntityToDataObjectProfile : Profile
    {
        public EntityToDataObjectProfile()
        {
            CreateMap<HazardEntity, HazardDataObject>(MemberList.None)
               .ReverseMap();
            CreateMap<JobEntity, JobDataObject>(MemberList.None)
                .ForMember(dest => dest.ServerK, opt => opt.MapFrom(src => src.JobK))
                .ReverseMap();
            CreateMap<SiteEntity, SiteDataObject>(MemberList.None)
                .ForMember(dest => dest.ServerK, opt => opt.MapFrom(src => src.SiteK))
                .ReverseMap();
            CreateMap<ConfigEntity, ConfigDataObject>(MemberList.None)
                .ForMember(dest => dest.ServerK, opt => opt.MapFrom(src => src.ConfigK))
                .ReverseMap();
            CreateMap<Entities.Configuration.ConfigInfoEntity, ConfigInfoDataObject>(MemberList.None)
               .ForMember(dest => dest.ServerK, opt => opt.MapFrom(src => src.ConfigInfoK))
               .ReverseMap();
            CreateMap<Entities.Configuration.JobTabEntity, JobTabDataObject>(MemberList.None)
                .ForMember(dest => dest.ServerK, opt => opt.MapFrom(src => src.JobTabK))
                .ReverseMap();
            CreateMap<Entities.Configuration.SectionEntity, SectionDataObject>(MemberList.None)
                .ForMember(dest => dest.ServerK, opt => opt.MapFrom(src => src.SectionK))
                .ReverseMap();
            CreateMap<SectionStepperEntity, SectionStepperDataObject>(MemberList.None)
                .ForMember(dest => dest.ServerK, opt => opt.MapFrom(src => src.SectionStepperK))
                .ReverseMap();
            CreateMap<Entities.Configuration.StepperEntity, StepperDataObject>(MemberList.None)
                .ForMember(dest => dest.ServerK, opt => opt.MapFrom(src => src.StepperK))
                .ReverseMap();
            CreateMap<ActionEntity, ActionDataObject>(MemberList.None)
                .ForMember(dest => dest.ServerK, opt => opt.MapFrom(src => src.ActionK))
                .ReverseMap();
            CreateMap<PreConditionEntity, PreConditionDataObject>(MemberList.None)
                .ForMember(dest => dest.ServerK, opt => opt.MapFrom(src => src.PreConditionK))
                .ReverseMap();
            CreateMap<ResponseEntity, ResponseDataObject>(MemberList.None)
                .ForMember(dest => dest.ServerK, opt => opt.MapFrom(src => src.ResponseK))
                .ReverseMap();
            CreateMap<ResponseDataEntity, ResponseDataDataObject>(MemberList.None)
                .ForMember(dest => dest.ServerK, opt => opt.MapFrom(src => src.ResponseDataK))
                .ReverseMap();
            CreateMap<FileStorageEntity, FileStorageDataObject>(MemberList.None)
                .ForMember(dest => dest.ServerK, opt => opt.MapFrom(src => src.FileStorageK))
                .ReverseMap();
        }
    }
}

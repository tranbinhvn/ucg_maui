using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCG.siteTRAXLite.DataContracts.UserInfoContracts;
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
    public class DataObjectToEntityProfile : Profile
    {
        public DataObjectToEntityProfile()
        {
            CreateMap<HazardDataObject, HazardEntity>(MemberList.None)
               .ReverseMap();
            CreateMap<JobDataObject, JobEntity>(MemberList.None)
                .ForMember(dest => dest.JobK, opt => opt.MapFrom(src => src.ServerK))
                .ReverseMap();
            CreateMap<SiteDataObject, SiteEntity>(MemberList.None)
                .ForMember(dest => dest.SiteK, opt => opt.MapFrom(src => src.ServerK))
                .ReverseMap();
            CreateMap<ConfigDataObject, ConfigEntity>(MemberList.None)
                .ForMember(dest => dest.ConfigK, opt => opt.MapFrom(src => src.ServerK))
                .ReverseMap();
            CreateMap<ConfigInfoDataObject, Entities.Configuration.ConfigInfoEntity>(MemberList.None)
               .ForMember(dest => dest.ConfigInfoK, opt => opt.MapFrom(src => src.ServerK))
               .ReverseMap();
            CreateMap<JobTabDataObject, Entities.Configuration.JobTabEntity>(MemberList.None)
                .ForMember(dest => dest.JobTabK, opt => opt.MapFrom(src => src.ServerK))
                .ReverseMap();
            CreateMap<SectionDataObject, Entities.Configuration.SectionEntity>(MemberList.None)
                .ForMember(dest => dest.SectionK, opt => opt.MapFrom(src => src.ServerK))
                .ReverseMap();
            CreateMap<SectionStepperDataObject, SectionStepperEntity>(MemberList.None)
                .ForMember(dest => dest.SectionStepperK, opt => opt.MapFrom(src => src.ServerK))
                .ReverseMap();
            CreateMap<StepperDataObject, Entities.Configuration.StepperEntity>(MemberList.None)
                .ForMember(dest => dest.StepperK, opt => opt.MapFrom(src => src.ServerK))
                .ReverseMap();
            CreateMap<ActionDataObject, ActionEntity>(MemberList.None)
                .ForMember(dest => dest.ActionK, opt => opt.MapFrom(src => src.ServerK))
                .ReverseMap();
            CreateMap<PreConditionDataObject, PreConditionEntity>(MemberList.None)
                .ForMember(dest => dest.PreConditionK, opt => opt.MapFrom(src => src.ServerK))
                .ReverseMap();
            CreateMap<ResponseDataObject, ResponseEntity>(MemberList.None)
                .ForMember(dest => dest.ResponseK, opt => opt.MapFrom(src => src.ServerK))
                .ReverseMap();
            CreateMap<ResponseDataDataObject, ResponseDataEntity>(MemberList.None)
                .ForMember(dest => dest.ResponseDataK, opt => opt.MapFrom(src => src.ServerK))
                .ReverseMap();
            CreateMap<FileStorageDataObject, FileStorageEntity>(MemberList.None)
                .ForMember(dest => dest.FileStorageK, opt => opt.MapFrom(src => src.ServerK))
                .ReverseMap();
        }
    }
}

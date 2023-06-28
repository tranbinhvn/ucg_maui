using AutoMapper;
using UCG.siteTRAXLite.DataContracts.UserInfoContracts;
using UCG.siteTRAXLite.Entities;

namespace UCG.siteTRAXLite.Managers.Mappers
{
    public class DataContractToEntityProfile : Profile
    {
        public DataContractToEntityProfile()
        {
            CreateMap<UserInfoDto, UserInfoEntity>(MemberList.None)
                .ReverseMap();
        }
    }
}

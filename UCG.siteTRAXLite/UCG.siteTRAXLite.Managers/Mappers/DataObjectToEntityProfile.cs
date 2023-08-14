using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCG.siteTRAXLite.DataContracts.UserInfoContracts;
using UCG.siteTRAXLite.DataObjects;
using UCG.siteTRAXLite.Entities;
using UCG.siteTRAXLite.Entities.SorEforms;

namespace UCG.siteTRAXLite.Managers.Mappers
{
    public class DataObjectToEntityProfile : Profile
    {
        public DataObjectToEntityProfile()
        {
            CreateMap<HazardDataObject, HazardEntity>(MemberList.None)
               .ReverseMap();
        }
    }
}

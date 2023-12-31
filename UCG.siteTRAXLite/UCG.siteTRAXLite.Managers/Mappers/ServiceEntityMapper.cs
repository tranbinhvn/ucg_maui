﻿using AutoMapper;

namespace UCG.siteTRAXLite.Managers.Mappers
{
    public class ServiceEntityMapper : IServiceEntityMapper
    {
        private MapperConfiguration config;

        public ServiceEntityMapper()
        {
            config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DataContractToEntityProfile>();
                cfg.AddProfile<DataObjectToEntityProfile>();
                cfg.AddProfile<EntityToDataObjectProfile>();
            });

            config.AssertConfigurationIsValid();

            Mapper = config.CreateMapper();
        }

        public IMapper Mapper { get; set; }

        public TDestination Map<TDestination>(object value)
        {
            return Mapper.Map<TDestination>(value);

        }

        public TDestination Map<TSource, TDestination>(TSource value)
        {
            return Mapper.Map<TSource, TDestination>(value);
        }
    }
}

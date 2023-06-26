using AutoMapper;

namespace UCG.siteTRAXLite.Mappers
{
    public interface IServiceEntityMapper
    {
        IMapper Mapper { get; set; }

        TDestination Map<TSource, TDestination>(TSource value);

        TDestination Map<TDestination>(object value);
    }
}

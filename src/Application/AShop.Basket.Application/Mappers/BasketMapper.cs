using AutoMapper;

namespace AShop.Basket.Application.Mappers;

public class BasketMapper
{
    private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
    {
        var config = new MapperConfiguration(x =>
        {
            x.ShouldMapProperty = info => info.GetMethod.IsPublic || info.GetMethod.IsAssembly;
            x.AddProfile<BasketMapperProfile>();
        });
        var mapper = config.CreateMapper();
        return mapper;
    });

    public static IMapper Mapper => Lazy.Value;
}
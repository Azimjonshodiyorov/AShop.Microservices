using AutoMapper;

namespace AShop.Catalog.Application.Mappers;

public static class ProductMapper 
{
    private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
    {
        var conf = new MapperConfiguration(co =>
        {
            co.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
            co.AddProfile<ProductMapperProfile>();
        });
        return conf.CreateMapper();
    });

    public static IMapper Mapper = Lazy.Value;
}
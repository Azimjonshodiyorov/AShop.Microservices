using System.Reflection;
using AShop.Catalog.Application.Handlers;
using AShop.Catalog.Infrastructure.Data;
using AShop.Catalog.Infrastructure.Repositories;
using AShop.Catalog.Infrastructure.Repositories.Interfaces;
using AShop.Common.Logging.Correlation;

namespace AShop.Catalog.WebAPI;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddMediatR(x=>
            x.RegisterServicesFromAssembly(typeof(CreateProductHandler).GetTypeInfo().Assembly));
        service.AddScoped<ICorrelationIdGenerator, CorrelationIdGenerator>();
        service.AddScoped<ICatalogContext, CatalogContext>();
        
        service.AddScoped<IProductRepository, ProductRepository>();
        service.AddScoped<ITypeRepository, ProductRepository>();
        service.AddScoped<IBrandRepository, ProductRepository>();
        
        return service;
    }
}
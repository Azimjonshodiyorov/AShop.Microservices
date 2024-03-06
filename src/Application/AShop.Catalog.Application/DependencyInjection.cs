using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AShop.Catalog.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection service, IConfiguration configuration)
    {
        return service;
    }
}
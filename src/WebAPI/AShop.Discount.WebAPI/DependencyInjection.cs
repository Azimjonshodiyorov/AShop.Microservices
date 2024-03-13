using System.Reflection;
using AShop.Common.Logging.Correlation;
using AShop.Discount.Application.Handlers;
using AShop.Discount.Infrastructure.Repositories;
using MediatR;

namespace AShop.Discount.WebAPI;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection collection, IConfiguration configuration)
    {
        collection.AddMediatR(typeof(CreateDiscountCommandHandler).GetTypeInfo().Assembly);
        collection.AddScoped<ICorrelationIdGenerator, CorrelationIdGenerator>();
        collection.AddScoped<IDiscountRepository, DiscountRepasitory>();
        collection.AddAutoMapper(typeof(Program));
        collection.AddGrpc();
        return collection;
    }
}
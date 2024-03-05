using AShop.Catalog.Domain.Entities;
using MongoDB.Driver;

namespace AShop.Catalog.Infrastructure.Data;

public interface ICatalogContext
{
    IMongoCollection<Product>  Products { get; }
    IMongoCollection<ProductType> ProductTypes { get; }
    IMongoCollection<ProductBrand> ProductBrands { get; }
}
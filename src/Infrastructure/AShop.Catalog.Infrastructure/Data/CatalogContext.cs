using AShop.Catalog.Domain.Entities;
using MongoDB.Driver;

namespace AShop.Catalog.Infrastructure.Data;

public class CatalogContext : ICatalogContext
{
    public IMongoCollection<Product> Products { get; }
    public IMongoCollection<ProductType> ProductTypes { get; }
    public IMongoCollection<ProductBrand> ProductBrands { get; }
    
}
using AShop.Catalog.Domain.Entities;
using AShop.Catalog.Domain.Specs;

namespace AShop.Catalog.Infrastructure.Repositories.Interfaces;

public interface IProductRepository
{
    ValueTask<Pagination<Product>> GetProductsAsync(CatalogSpecsParams specsParams);
    ValueTask<Product> GetByIdProductAsync(string id);
    ValueTask<IEnumerable<Product>> GetByNameProductAsync(string name);
    ValueTask<IEnumerable<Product>> GetByBrandProductAsync(string name);
    ValueTask<Product> CreateProductAsync(Product product);
    ValueTask<bool> UpdateProductAsync(Product product);
    ValueTask<bool> DeleteProductAsync(string id);
}
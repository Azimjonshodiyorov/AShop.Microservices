using AShop.Catalog.Domain.Entities;

namespace AShop.Catalog.Infrastructure.Repositories.Interfaces;

public interface IBrandRepository
{
    ValueTask<IEnumerable<ProductBrand>> GetAllBrands();
}
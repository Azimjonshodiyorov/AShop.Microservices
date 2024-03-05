using AShop.Catalog.Domain.Entities;

namespace AShop.Catalog.Infrastructure.Repositories.Interfaces;

public interface ITypeRepository
{
    ValueTask<IEnumerable<ProductType>> GetAllTypes();
}
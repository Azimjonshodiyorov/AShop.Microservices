using AShop.Catalog.Domain.Entities;
using AShop.Catalog.Domain.Specs;
using AShop.Catalog.Infrastructure.Data;
using AShop.Catalog.Infrastructure.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AShop.Catalog.Infrastructure.Repositories;

public class ProductRepository : IProductRepository , IBrandRepository , ITypeRepository
{
    private readonly ICatalogContext _catalogContext;

    public ProductRepository(ICatalogContext catalogContext)
    {
        _catalogContext = catalogContext;
    }


    private async ValueTask<IReadOnlyList<Product>> DataFilter(CatalogSpecsParams specsParams,
        FilterDefinition<Product> filterDefinition)
    {
        switch (specsParams.Sort)
        {
            case "priceAsc":
                return await this._catalogContext
                    .Products
                    .Find(filterDefinition)
                    .Sort(Builders<Product>.Sort.Ascending("Price"))
                    .Skip(specsParams.PageSize * (specsParams.PageIndex - 1))
                    .Limit(specsParams.PageSize)
                    .ToListAsync();
            case "priceDesc":
                return await this._catalogContext
                    .Products
                    .Find(filterDefinition)
                    .Sort(Builders<Product>.Sort.Descending("Price"))
                    .Skip(specsParams.PageSize * (specsParams.PageIndex - 1))
                    .Limit(specsParams.PageSize)
                    .ToListAsync();
            default:
                return await this._catalogContext
                    .Products
                    .Find(filterDefinition)
                    .Sort(Builders<Product>.Sort.Ascending("Price"))
                    .Skip(specsParams.PageSize * (specsParams.PageIndex - 1))
                    .Limit(specsParams.PageSize)
                    .ToListAsync();
        }
    }
    public async ValueTask<Pagination<Product>> GetProductsAsync(CatalogSpecsParams specsParams)
    {
        var builder = Builders<Product>.Filter;
        var filter = builder.Empty;
        if (!string.IsNullOrEmpty(specsParams.Search))
        {
            var searchFilter = builder.Regex(x => x.Name, new BsonRegularExpression(specsParams.Search));
            filter &= searchFilter;
        }

        if (!string.IsNullOrEmpty(specsParams.BrandId))
        {
            var brandFilter = builder.Eq(x => x.Brands.Id, specsParams.BrandId);
            filter &= brandFilter;
        }

        if (!string.IsNullOrEmpty(specsParams.TypeId))
        {
            var typeFilter = builder.Eq(x => x.Types.Id, specsParams.TypeId);
            filter &= typeFilter;
        }

        if (!string.IsNullOrEmpty(specsParams.Sort))
        {
            return new Pagination<Product>()
            {
                PageSize = specsParams.PageSize,
                PagIndex = specsParams.PageIndex,
                Data = await DataFilter(specsParams, filter),
                Count = await this._catalogContext.Products.CountDocumentsAsync(x => true)
            };
        }

        return new Pagination<Product>()
        {
            PageSize = specsParams.PageSize,
            PagIndex = specsParams.PageIndex,
            Data = await this._catalogContext
                .Products
                .Find(filter)
                .Sort(Builders<Product>.Sort.Ascending("Name"))
                .Skip(specsParams.PageSize * (specsParams.PageIndex - 1))
                .Limit(specsParams.PageSize)
                .ToListAsync(),
            Count = await this._catalogContext.Products.CountDocumentsAsync(x=>true),
        };
    }

    public async ValueTask<Product> GetByIdProductAsync(string id)
    {
        return await this._catalogContext
            .Products
            .Find(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public async ValueTask<IEnumerable<Product>> GetByNameProductAsync(string name)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Name , name);
        return await this._catalogContext
            .Products
            .Find(filter)
            .ToListAsync();
    }

    public async ValueTask<IEnumerable<Product>> GetByBrandProductAsync(string name)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Brands.Name, name);
        return await this._catalogContext
            .Products
            .Find(filter)
            .ToListAsync();
    }

    public async ValueTask<Product> CreateProductAsync(Product product)
    {
        await this._catalogContext.Products.InsertOneAsync(product);
        return product;
    }

    public async ValueTask<bool> UpdateProductAsync(Product product)
    {
        var updateResult = await this._catalogContext
            .Products
            .ReplaceOneAsync(x => x.Id == product.Id, product);
        return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
    }

    public async ValueTask<bool> DeleteProductAsync(string id)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Id, id);
        DeleteResult deleteResult = await this._catalogContext
            .Products
            .DeleteOneAsync(filter);
        return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
    }

    public async ValueTask<IEnumerable<ProductBrand>> GetAllBrands()
    {
        return await this._catalogContext
            .ProductBrands
            .Find(x => true)
            .ToListAsync();
    }

    public async ValueTask<IEnumerable<ProductType>> GetAllTypes()
    {
        return await this._catalogContext
            .ProductTypes
            .Find(x => true)
            .ToListAsync();
    }
}
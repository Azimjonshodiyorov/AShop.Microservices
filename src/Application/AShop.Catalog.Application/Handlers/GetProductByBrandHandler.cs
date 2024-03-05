using AShop.Catalog.Application.Mappers;
using AShop.Catalog.Application.Queries;
using AShop.Catalog.Application.Responses;
using AShop.Catalog.Infrastructure.Repositories.Interfaces;
using MediatR;

namespace AShop.Catalog.Application.Handlers;

public class GetProductByBrandHandler : IRequestHandler<GetProductBrandByQuery , IList<ProductRespons>>
{
    private readonly IProductRepository _productRepository;

    public GetProductByBrandHandler( IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<IList<ProductRespons>> Handle(GetProductBrandByQuery request, CancellationToken cancellationToken)
    {
        var productList = await this._productRepository.GetByBrandProductAsync(request.BrandName);
        var productResponeList = ProductMapper.Mapper.Map<IList<ProductRespons>>(productList);
        return productResponeList;
    }
}
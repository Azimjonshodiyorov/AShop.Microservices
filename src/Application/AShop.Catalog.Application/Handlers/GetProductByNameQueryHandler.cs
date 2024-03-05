using AShop.Catalog.Application.Mappers;
using AShop.Catalog.Application.Queries;
using AShop.Catalog.Application.Responses;
using AShop.Catalog.Infrastructure.Repositories.Interfaces;
using MediatR;

namespace AShop.Catalog.Application.Handlers;

public class GetProductByNameQueryHandler : IRequestHandler<GetProductByNameQuery , IList<ProductRespons>>
{
    private readonly IProductRepository _productRepository;

    public GetProductByNameQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<IList<ProductRespons>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
    {
        var productList = await this._productRepository.GetByNameProductAsync(request.Name);
        var productResponeList = ProductMapper.Mapper.Map<IList<ProductRespons>>(productList);
        return productResponeList;
    }
}
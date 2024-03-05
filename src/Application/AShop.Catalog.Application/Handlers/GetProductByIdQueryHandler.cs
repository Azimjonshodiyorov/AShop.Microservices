using AShop.Catalog.Application.Mappers;
using AShop.Catalog.Application.Queries;
using AShop.Catalog.Application.Responses;
using AShop.Catalog.Infrastructure.Repositories.Interfaces;
using MediatR;

namespace AShop.Catalog.Application.Handlers;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery , ProductRespons>
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<ProductRespons> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await this._productRepository.GetByIdProductAsync(request.Id);
        var productRespone = ProductMapper.Mapper.Map<ProductRespons>(product);
        return productRespone;
    }
}
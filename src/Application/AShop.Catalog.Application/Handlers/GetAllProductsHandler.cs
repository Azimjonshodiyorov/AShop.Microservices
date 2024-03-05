using AShop.Catalog.Application.Mappers;
using AShop.Catalog.Application.Queries;
using AShop.Catalog.Application.Responses;
using AShop.Catalog.Domain.Specs;
using AShop.Catalog.Infrastructure.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AShop.Catalog.Application.Handlers;

public class GetAllProductsHandler :IRequestHandler<GetAllProductQuery , Pagination<ProductRespons>>
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<GetAllProductsHandler> _logger;

    public GetAllProductsHandler(IProductRepository productRepository , ILogger<GetAllProductsHandler> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }
    public async Task<Pagination<ProductRespons>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        var productList = await this._productRepository.GetProductsAsync(request.CatalogSpecsParams);
        var productResponseList = ProductMapper.Mapper.Map<Pagination<ProductRespons>>(productList);
        this._logger.LogDebug("Qabul qilingan mahsulotlar roʻyxati. Umumiy soni : {productList}", productResponseList.Count);
        return productResponseList;
    }
}
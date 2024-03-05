using AShop.Catalog.Application.Commands;
using AShop.Catalog.Application.Mappers;
using AShop.Catalog.Application.Responses;
using AShop.Catalog.Domain.Entities;
using AShop.Catalog.Infrastructure.Repositories.Interfaces;
using MediatR;

namespace AShop.Catalog.Application.Handlers;

public class CreateProductHandler : IRequestHandler<CreateProductCommand , ProductRespons>
{
    private readonly IProductRepository _productRepository;

    public CreateProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<ProductRespons> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var productEntity = ProductMapper.Mapper.Map<Product>(request);
        if (productEntity is null)
        {
            throw new ApplicationException("Yangi mahsulotni yaratishda Mappingda muammo bor");
        }

        var newProduct = await this._productRepository.CreateProductAsync(productEntity);
        var productResponse = ProductMapper.Mapper.Map<ProductRespons>(newProduct);
        return productResponse;
    }
}
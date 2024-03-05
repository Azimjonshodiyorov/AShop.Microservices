using AShop.Catalog.Application.Commands;
using AShop.Catalog.Domain.Entities;
using AShop.Catalog.Infrastructure.Repositories.Interfaces;
using MediatR;

namespace AShop.Catalog.Application.Handlers;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand , bool>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var productEntity = await this._productRepository.UpdateProductAsync(new Product()
        {
            Id = request.Id,
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Summary = request.Summary,
            ImageFile = request.ImageFile,
            Brands = request.Brands,
            Types = request.Types,
        });
        return productEntity;
    }
}
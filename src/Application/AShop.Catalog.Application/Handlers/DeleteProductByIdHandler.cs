using AShop.Catalog.Application.Queries;
using AShop.Catalog.Infrastructure.Repositories.Interfaces;
using MediatR;

namespace AShop.Catalog.Application.Handlers;

public class DeleteProductByIdHandler : IRequestHandler<DeleteProductByIdQuery , bool>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductByIdHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<bool> Handle(DeleteProductByIdQuery request, CancellationToken cancellationToken)
    {
        return await this._productRepository.DeleteProductAsync(request.Id);
    }
}
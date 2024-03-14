using AShop.Basket.Application.Queries;
using AShop.Basket.Infrastructure.Repositories;
using MediatR;

namespace AShop.Basket.Application.Handlers;

public class DeleteBasketByUserNameHandler : IRequestHandler<DeleteBasketByUserNameQuery>
{
    private readonly IBasketRepository _basketRepository;

    public DeleteBasketByUserNameHandler(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }
    public async Task<Unit> Handle(DeleteBasketByUserNameQuery request, CancellationToken cancellationToken)
    {
        await this._basketRepository.DeleteBasket(request.UserName);
        return Unit.Value;
    }
}
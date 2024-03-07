using AShop.Basket.Application.Mappers;
using AShop.Basket.Application.Queries;
using AShop.Basket.Application.Responses;
using AShop.Basket.Infrastructure.Repositories;
using MediatR;

namespace AShop.Basket.Application.Handlers;

public class GetBasketByUserNameHandler : IRequestHandler<GetBasketByUserNameQuery ,ShoppingCartResponse>
{
    private readonly IBasketRepository _basketRepository;

    public GetBasketByUserNameHandler(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }
    public async Task<ShoppingCartResponse> Handle(GetBasketByUserNameQuery request, CancellationToken cancellationToken)
    {
        var shoppingCart = await this._basketRepository.GetBasket(request.UserName);
        var shoppingCartResponse = BasketMapper.Mapper.Map<ShoppingCartResponse>(shoppingCart);
        return shoppingCartResponse;
    }
}
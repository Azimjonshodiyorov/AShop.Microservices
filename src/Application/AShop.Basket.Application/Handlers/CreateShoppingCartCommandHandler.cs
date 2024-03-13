using AShop.Basket.Application.Commands;
using AShop.Basket.Application.GrpcService;
using AShop.Basket.Application.Mappers;
using AShop.Basket.Application.Responses;
using AShop.Basket.Domain.Entities;
using AShop.Basket.Infrastructure.Repositories;
using MediatR;

namespace AShop.Basket.Application.Handlers;

public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand , ShoppingCartResponse>
{
    private readonly IBasketRepository _repository;
    private readonly DiscountGrpcService _grpcService;

    public CreateShoppingCartCommandHandler(IBasketRepository repository , DiscountGrpcService grpcService)
    {
        _repository = repository;
        _grpcService = grpcService;
    }

    public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
    {
        foreach (var item in request.CartItems)
        {
            var coupon = await this._grpcService.GetDiscount(item.ProductName);
            item.Price -= coupon.Amount;
        }

        var shoppingCart = await this._repository.UpdateBasket(new ShoppingCart()
        {
            UserName = request.UserName,
            ShoppingCartItems = request.CartItems
        });
        var shoppingCartResponse = BasketMapper.Mapper.Map<ShoppingCartResponse>(shoppingCart);
        return shoppingCartResponse;
    }
}
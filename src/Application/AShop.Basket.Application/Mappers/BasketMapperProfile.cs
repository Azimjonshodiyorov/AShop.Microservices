using AShop.Basket.Application.Responses;
using AShop.Basket.Domain.Entities;
using AShop.EventBus.Message.Events;
using AutoMapper;

namespace AShop.Basket.Application.Mappers;

public class BasketMapperProfile : Profile
{
    public BasketMapperProfile()
    {
        CreateMap<ShoppingCart, ShoppingCartResponse>().ReverseMap();
        CreateMap<ShoppingCartItem, ShoppingCartItemResponse>().ReverseMap();
        CreateMap<BasketCheckoutV2, BasketCheckoutEvent>().ReverseMap();
        CreateMap<BasketCheckout, BasketCheckoutEventV2>().ReverseMap();
    }
}
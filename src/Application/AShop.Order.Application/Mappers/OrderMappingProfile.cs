using AShop.EventBus.Message.Events;
using AShop.Order.Application.Commands;
using AShop.Order.Application.Responses;
using AutoMapper;

namespace AShop.Order.Application.Mappers;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<Domain.Entities.Order, OrderResponse>().ReverseMap();
        CreateMap<Domain.Entities.Order, CheckoutOrderCommand>().ReverseMap();
        CreateMap<Domain.Entities.Order, UpdateOrderCommand>().ReverseMap();
        CreateMap<CheckoutOrderCommand, BasketCheckoutEvent>().ReverseMap();
        CreateMap<CheckoutOrderCommand, BasketCheckoutEventV2>().ReverseMap();
    }
}
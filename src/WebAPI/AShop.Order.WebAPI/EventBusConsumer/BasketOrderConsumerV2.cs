using AShop.EventBus.Message.Events;
using AShop.Order.Application.Commands;
using AutoMapper;
using MassTransit;
using MediatR;

namespace AShop.Order.WebAPI.EventBusConsumer;

public class BasketOrderConsumerV2 : IConsumer<BasketCheckoutEventV2>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly Logger<BasketOrderConsumerV2> _logger;

    public BasketOrderConsumerV2(IMediator mediator , IMapper mapper , Logger<BasketOrderConsumerV2> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task Consume(ConsumeContext<BasketCheckoutEventV2> context)
    {
        using var scope =  _logger.BeginScope("Consuming Basket Checkout Event for {correlationId}",
            context.Message.CorrelationId);
        var common = this._mapper.Map<CheckoutOrderCommand>(context.Message);
 
        PopulateAddressDetails(common);
        var result = await _mediator.Send(common);
        
        _logger.LogInformation($"Basket checkout event completed!!!");
    }
    private static void PopulateAddressDetails(CheckoutOrderCommand command)
    {
        command.FirstName = "Azimjon";
        command.LastName = "Shodiyorov";
        command.EmailAddress = "azimjonshodiyorov@gmail.com";
        command.AddressLine = "Yunsabod";
        command.Country = "Uzb";
        command.State = "KA";
        command.ZipCode = "560001";
        command.PaymentMethod = 1;
        command.CardName = "Visa";
        command.CardNumber = "1234567890123456";
        command.Expiration = "12/25";
        command.CVV = "123";
    }
}
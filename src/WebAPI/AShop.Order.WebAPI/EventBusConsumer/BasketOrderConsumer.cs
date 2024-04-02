using AShop.EventBus.Message.Events;
using AShop.Order.Application.Commands;
using AutoMapper;
using MassTransit;
using MediatR;

namespace AShop.Order.WebAPI.EventBusConsumer;

public class BasketOrderConsumer : IConsumer<BasketCheckoutEvent>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<BasketOrderConsumer> _logger;

    public BasketOrderConsumer(IMediator mediator , IMapper mapper , ILogger<BasketOrderConsumer> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        using var scope = _logger.BeginScope("Consuming Basket Checkout Event for {correlationId}",
            context.Message.CorrelationId);
        var command = _mapper.Map<CheckoutOrderCommand>(context.Message);
        var result = await this._mediator.Send(command);
        _logger.LogInformation($"Basket checkout event completed");
    }
}
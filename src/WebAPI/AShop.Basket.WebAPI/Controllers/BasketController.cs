using System.Net;
using AShop.Basket.Application.Commands;
using AShop.Basket.Application.Mappers;
using AShop.Basket.Application.Queries;
using AShop.Basket.Application.Responses;
using AShop.Basket.Domain.Entities;
using AShop.Common.Logging.Correlation;
using AShop.EventBus.Message.Events;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AShop.Basket.WebAPI.Controllers;

public class BasketController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IPublishEndpoint _endpoint;
    private readonly ILogger<BasketController> _logger;
    private readonly ICorrelationIdGenerator _correlationIdGenerator;

    public BasketController(IMediator mediator , IPublishEndpoint endpoint , ILogger<BasketController> logger , ICorrelationIdGenerator correlationIdGenerator)
    {
        _mediator = mediator;
        _endpoint = endpoint;
        _logger = logger;
        _correlationIdGenerator = correlationIdGenerator;
        _logger.LogInformation("CorrelationId {correlationId}:", _correlationIdGenerator.Get());
    }

    [HttpGet]
    [Route("[action]/{userName}" , Name = "GetBasketByUserName")]
    [ProducesResponseType(typeof(ShoppingCartResponse) , (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCartResponse>> GetBasket(string userName)
    {
        var query = new GetBasketByUserNameQuery(userName);
        var basket = await this._mediator.Send(query);
        return Ok(basket);
    }


    [HttpPost("CreateBasket")]
    [ProducesResponseType(typeof(ShoppingCartResponse) , (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCartResponse>> CreateBasket(
        [FromBody] CreateShoppingCartCommand shoppingCartCommand)
    {
        var basket = await this._mediator.Send(shoppingCartCommand);
        return Ok(basket);
    }

    [HttpDelete]
    [Route("[action]/{userName}" , Name = "DeleteBasketByUserName")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCartResponse>> DeleteBasket(string userName)
    {
        var query = new DeleteBasketByUserNameQuery(userName);
        var result = await this._mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Chekout([FromBody] BasketCheckout checkout)
    {
        var query = new GetBasketByUserNameQuery(checkout.UserName);
        var basket = await this._mediator.Send(query);

        if (basket is null)
            return BadRequest();
        var eventMess = BasketMapper.Mapper.Map<BasketCheckoutEvent>(checkout);
        eventMess.TotalPrice = checkout.TotalPrice;
        eventMess.CorrelationId = this._correlationIdGenerator.Get();

        await this._endpoint.Publish(eventMess);
        var deleteQuery = new DeleteBasketByUserNameQuery(checkout.UserName);
        await this._mediator.Send(deleteQuery);

        return Accepted();
    }
}
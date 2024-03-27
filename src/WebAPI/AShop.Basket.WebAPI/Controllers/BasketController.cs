using System.Net;
using AShop.Basket.Application.Commands;
using AShop.Basket.Application.Queries;
using AShop.Basket.Application.Responses;
using AShop.Common.Logging.Correlation;
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
    
    
}
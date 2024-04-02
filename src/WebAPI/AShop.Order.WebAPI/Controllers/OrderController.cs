using System.Net;
using AShop.Order.Application.Commands;
using AShop.Order.Application.Queries;
using AShop.Order.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AShop.Order.WebAPI.Controllers;

public class OrderController : ApiController
{
    private readonly IMediator _mediator;
    private readonly ILogger<OrderController> _logger;

    public OrderController(IMediator mediator , ILogger<OrderController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("{userName}" , Name = "GetOrdersByUserName")]
    [ProducesResponseType(typeof(IEnumerable<OrderResponse>) , (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<OrderResponse>>> GetOrdersByUserName(string userName)
    {
        var query = new GetOrderListQuery(userName);
        var result = await this._mediator.Send(query);
        return Ok(result);
    }

    [HttpPost(Name = "CheckoutOrder")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<long>> CheckoutOrder([FromBody] CheckoutOrderCommand command)
    {
        var result = await this._mediator.Send(command);
        return Ok(result);
    }

    [HttpPut(Name = "UpdateOrder")]
    [ProducesResponseType(StatusCodes.Status205ResetContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> UpdateOrder([FromBody] UpdateOrderCommand command)
    {
        var result = await this._mediator.Send(command);
        return Ok(result);
    }


    [HttpDelete("{id}" , Name = "DeleteOrder")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> DeleteOrder(long id)
    {
        var delete = new DeleteOrderCommand() { Id = id };
        await this._mediator.Send(delete);
        return NoContent();
    }
}
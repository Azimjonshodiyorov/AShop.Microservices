using System.Net;
using AShop.Catalog.Application.Queries;
using AShop.Catalog.Application.Responses;
using AShop.Catalog.Domain.Specs;
using AShop.Common.Logging.Correlation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AShop.Catalog.WebAPI.Controllers;

public class CatalogController : ApiController
{
    private readonly IMediator _mediator;
    private readonly ILogger<CatalogController> _logger;
    private readonly ICorrelationIdGenerator _correlationIdGenerator;

    public CatalogController(IMediator mediator , ILogger<CatalogController> logger , ICorrelationIdGenerator correlationIdGenerator) 
    {
        _mediator = mediator;
        _logger = logger;
        _correlationIdGenerator = correlationIdGenerator;
        _logger.LogInformation("CorrelationId {correlationId}:",_correlationIdGenerator.Get());
    }

    [HttpGet]
    [Route("GetAllProducts")]
    [ProducesResponseType(typeof(IList<ProductRespons>) , (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IList<ProductRespons>>> GetAllProducts([FromQuery] CatalogSpecsParams catalogSpecsParams)
    {
        try
        {
            var query = new GetAllProductQuery(catalogSpecsParams);
            var result = await this._mediator.Send(query);
            _logger.LogInformation("All products olindi");
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError("Xatolik  yuz berdi {Exception}" );
            throw;
        }
    }
}
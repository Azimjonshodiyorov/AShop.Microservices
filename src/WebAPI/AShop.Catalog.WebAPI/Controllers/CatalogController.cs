using System.Net;
using AShop.Catalog.Application.Commands;
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

    [HttpGet]
    [Route("[action]/{id}", Name = "GetProductById")]
    [ProducesResponseType(typeof(ProductRespons),(int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ProductRespons),(int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<ProductRespons>> GetProductById(string id)
    {
        var query = new GetProductByIdQuery(id);
        var result = await this._mediator.Send(query);
        return Ok(query);
    }

    [HttpGet]
    [Route("[action]/{name}" , Name = "GetProductByName")]
    [ProducesResponseType(typeof(IList<ProductRespons>) , (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IList<ProductRespons>>> GetProductsByName(string name)
    {
        var query = new GetProductByNameQuery(name);
        var result = await this._mediator.Send(query);
        return Ok(result);
    }

    [HttpGet]
    [Route("GetAllBrands")]
    [ProducesResponseType(typeof(IList<BrandRespons>) , (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IList<ProductRespons>>> GetAllBrands()
    {
        var query = new GetAllBrandsQuery();
        var result = await this._mediator.Send(query);
        return Ok(result);
    }

    [HttpGet]
    [Route("GetAllTypes")]
    [ProducesResponseType(typeof(IList<TypeRespons>) , (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IList<TypeRespons>>> GetAllTypes()
    {
        var query = new GetAllTypeQuery();
        var result = await this._mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet]
    [Route("[action]/{brandName}", Name = "GetProductByBrandName")]
    [ProducesResponseType(typeof(IList<BrandRespons>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IList<BrandRespons>>> GetProductByBrandName(string brandName)
    {
        var query = new GetProductByNameQuery(brandName);
        var result = await this._mediator.Send(query);
        return Ok(result);
    }
    
    [HttpPost]
    [Route("CreateProduct")]
    [ProducesResponseType(typeof(ProductRespons),(int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductRespons>> CreateProduct([FromBody] CreateProductCommand productCommand)
    {
        var result = await this._mediator.Send(productCommand);
        return Ok(result);
    }
    
    [HttpPut]
    [Route("UpdateProduct")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand productCommand)
    {
        var result = await this._mediator.Send(productCommand);
        return Ok(result);
    }

    [HttpDelete]
    [Route("{id}", Name = "DeleteProduct")]
    [ProducesResponseType(typeof(bool) , (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        var query = new DeleteProductByIdQuery(id);
        var result = await this._mediator.Send(query);
        return Ok(result);
    }
 }
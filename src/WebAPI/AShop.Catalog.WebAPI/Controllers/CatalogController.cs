using MediatR;

namespace AShop.Catalog.WebAPI.Controllers;

public class CatalogController : ApiController
{
    private readonly IMediator _mediator;
    private readonly ILogger<CatalogController> _logger;

    public CatalogController(IMediator mediator , ILogger<CatalogController> logger) 
    {
        _mediator = mediator;
        _logger = logger;
    }
}
using Microsoft.AspNetCore.Mvc;

namespace AShop.Catalog.WebAPI.Controllers;
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class ApiController : ControllerBase
{
    
}
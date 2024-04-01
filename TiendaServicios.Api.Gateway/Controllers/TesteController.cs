using Microsoft.AspNetCore.Mvc;

namespace TiendaServicios.Api.Gateway.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TesteController : ControllerBase
{
    [HttpGet]
    public IActionResult GetTest()
    {
        return Ok();
    }
}

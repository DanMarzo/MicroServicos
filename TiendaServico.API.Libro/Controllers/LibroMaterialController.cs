using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiendaServico.API.Libro.Application;
using TiendaServico.API.Libro.Models;

namespace TiendaServico.API.Libro.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class LibroMaterialController : ControllerBase
{
    private readonly IMediator _mediator;
    public LibroMaterialController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<ActionResult<LibreriaMaterial>> Crear(Nuevo.Ejecuta ejecuta)
    {
        var libreriaMaterial=  await _mediator.Send(ejecuta);
        return Ok(libreriaMaterial);
    }
}

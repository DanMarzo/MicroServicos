using MediatR;
using Microsoft.AspNetCore.Mvc;
using TiendaService.API.Autor.Application;
using TiendaService.API.Autor.Models;


namespace TiendaService.API.Autor.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AutorController : ControllerBase
{
    private readonly IMediator _mediator;
    public AutorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<AutorLibro>> Crear(Nuevo.Ejecuta ejecuta)
    {
        AutorLibro autorLibro = await _mediator.Send(ejecuta);
        return Ok(autorLibro);
    }
    [HttpGet]
    public async Task<ActionResult<List<AutorDTO>>> GetAutores()
    {
        var autorLibros = await _mediator.Send(new Consulta.ListaAutor());
        return Ok(autorLibros);
    }
    [HttpGet]
    public async Task<ActionResult<AutorDTO>> GetAutorLibro([FromQuery] string id)
    {
        return await _mediator.Send(
            new ConsultaFiltro.AutorUnico
            {
                AutorGuid = id,
            });
    }
}

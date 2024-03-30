using MediatR;
using Microsoft.AspNetCore.Mvc;
using TiendaServico.API.Libro.Application;
using TiendaServico.API.Libro.Application.DTOs;
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
        var libreriaMaterial = await _mediator.Send(ejecuta);
        return Ok(libreriaMaterial);
    }
    [HttpGet]
    public async Task<ActionResult<List<LibroMaterialDTO>>> GetLibros()
    {
        return await _mediator.Send(new Consulta.Ejecuta());
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<LibroMaterialDTO>> GetLibroUnico(Guid id)
    {
        return await _mediator.Send(new ConsultaFiltro.LibroUnico { LibroId = id });
    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using TiendaServicios.API.CarritoCompra.Application;
using TiendaServicios.API.CarritoCompra.Application.DTOs;
using TiendaServicios.API.CarritoCompra.Models;

namespace TiendaServicios.API.CarritoCompra.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CarritoComprasController : ControllerBase
{
    private readonly IMediator _mediator;
    public CarritoComprasController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<ActionResult<CarritoSession>> Crear(Nuevo.Ejecuta ejecuta)
    {
        return await _mediator.Send(ejecuta);
    }
    [HttpGet]
    public async Task<ActionResult<CarritoDTO>> GetCarrito([FromQuery] int id)
    {
        return await _mediator.Send(new Consulta.Ejecuta
        {
            CarritoSessionId = id
        });
    }

}

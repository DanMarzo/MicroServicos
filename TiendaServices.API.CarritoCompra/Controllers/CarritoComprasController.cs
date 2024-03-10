﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using TiendaServices.API.CarritoCompra.Application;
using TiendaServices.API.CarritoCompra.Models;

namespace TiendaServices.API.CarritoCompra.Controllers;

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
}

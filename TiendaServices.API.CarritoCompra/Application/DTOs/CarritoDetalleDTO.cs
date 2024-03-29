﻿namespace TiendaServices.API.CarritoCompra.Application.DTOs;

public class CarritoDetalleDTO
{
    public Guid? LibroId { get; set; }
    public string? TituloLibro { get; set; }
    public string? AutorLibro { get; set; }
    public DateTime? FechaPublicacion { get; set; }
}

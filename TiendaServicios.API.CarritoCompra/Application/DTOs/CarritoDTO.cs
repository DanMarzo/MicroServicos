namespace TiendaServicios.API.CarritoCompra.Application.DTOs;

public class CarritoDTO
{
    public int CarritoId { get; set; }
    public DateTime? FechaCreacionSession { get; set; }
    public List<CarritoDetalleDTO>? ListaProductos { get; set; }
}

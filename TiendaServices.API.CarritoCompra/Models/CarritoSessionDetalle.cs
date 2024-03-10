namespace TiendaServices.API.CarritoCompra.Models;

public class CarritoSessionDetalle
{
    public int CarritoSessionDetalleId { get; set; }
    public DateTime? FechaOperacion { get; set; }
    public string? ProductoSelecioado { get; set; }
    public int CarritoSessionId { get; set; }
    public CarritoSession? CarritoSession { get; set; }
}

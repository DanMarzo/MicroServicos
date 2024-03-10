namespace TiendaServices.API.CarritoCompra.Models;

public class CarritoSession
{
    public int? CarritoSessionId { get; set; }
    public DateTime? FechaCreacion { get; set; }
    public ICollection<CarritoSessionDetalle>? ListaDetalhe { get; set; }
}

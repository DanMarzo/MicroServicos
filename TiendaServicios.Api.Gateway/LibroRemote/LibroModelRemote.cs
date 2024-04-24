namespace TiendaServicios.Api.Gateway.LibroRemote;

public class LibroModelRemote
{
    public string? Titulo { get; set; }
    public DateTime? FechaPublicacion { get; set; }
    public Guid? AutorLibro { get; set; }
    public Guid? LibreriaMaterialId { get; set; }
}

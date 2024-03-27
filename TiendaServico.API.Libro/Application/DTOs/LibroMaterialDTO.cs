namespace TiendaServico.API.Libro.Application.DTOs;

public class LibroMaterialDTO
{
    public string? Titulo { get; set; }
    public DateTime? FechaPublicacion { get; set; }
    public Guid? AutorLibro { get; set; }
    public Guid? LibreriaMaterialId { get; set; }
}

namespace TiendaServico.API.Libro.Application;

public class LibreMaterialDTO
{
    public string? Titulo { get; set; }
    public DateTime? FechaPublicacion { get; set; }
    public Guid? AutorLibro { get; set; }
}

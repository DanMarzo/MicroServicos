using Microsoft.EntityFrameworkCore;
using TiendaService.API.Autor.Models;

namespace TiendaService.API.Autor.Persistence;

public class ContextAutor : DbContext
{
    public ContextAutor(DbContextOptions<ContextAutor> options) : base(options) {}
    public DbSet<AutorLibro> AutorLibros { get; set; }
    public DbSet<GradoAcademico> GradoAcademicos { get; set; }
}

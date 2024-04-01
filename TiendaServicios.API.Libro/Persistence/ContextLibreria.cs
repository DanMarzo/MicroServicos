using Microsoft.EntityFrameworkCore;
using TiendaServicios.API.Libro.Models;

namespace TiendaServicios.API.Libro.Persistence;

public class ContextLibreria : DbContext
{
    public ContextLibreria() { }
    public ContextLibreria(DbContextOptions<ContextLibreria> options) : base(options) { }
    public virtual DbSet<LibreriaMaterial> LibreriasMaterials { get; set; }
}

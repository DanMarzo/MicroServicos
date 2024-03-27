using Microsoft.EntityFrameworkCore;
using TiendaServico.API.Libro.Models;

namespace TiendaServico.API.Libro.Persistence;

public class ContextLibreria : DbContext
{
    public ContextLibreria() { }
    public ContextLibreria(DbContextOptions<ContextLibreria> options) : base(options) { }
    public virtual DbSet<LibreriaMaterial> LibreriasMaterials { get; set; }
}

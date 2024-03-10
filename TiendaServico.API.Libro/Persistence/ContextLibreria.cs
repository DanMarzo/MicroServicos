using Microsoft.EntityFrameworkCore;
using TiendaServico.API.Libro.Models;

namespace TiendaServico.API.Libro.Persistence;

public class ContextLibreria : DbContext
{
    public ContextLibreria(DbContextOptions<ContextLibreria> options) : base(options) { }
    public DbSet<LibreriaMaterial> LibreriasMaterials { get; set; }
}

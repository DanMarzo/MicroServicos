using Microsoft.EntityFrameworkCore;
using TiendaServices.API.CarritoCompra.Models;

namespace TiendaServices.API.CarritoCompra.Persistence;

public class ContextCarrito : DbContext
{
    public ContextCarrito(DbContextOptions<ContextCarrito> options) : base(options) { }

    public DbSet<CarritoSession> CarritoSession { get; set; }
    public DbSet<CarritoSessionDetalle> CarritoSessionDetalle { get; set; }
}

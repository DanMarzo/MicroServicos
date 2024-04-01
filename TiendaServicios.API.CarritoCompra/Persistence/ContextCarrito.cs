using Microsoft.EntityFrameworkCore;
using TiendaServicios.API.CarritoCompra.Models;

namespace TiendaServicios.API.CarritoCompra.Persistence;

public class ContextCarrito : DbContext
{
    public ContextCarrito(DbContextOptions<ContextCarrito> options) : base(options) { }

    public DbSet<CarritoSession> CarritoSession { get; set; }
    public DbSet<CarritoSessionDetalle> CarritoSessionDetalle { get; set; }
}

using TiendaServicios.API.CarritoCompra.RemoteModel;

namespace TiendaServicios.API.CarritoCompra.RemoteInterface;

public interface ILibroService
{
    Task<(bool resultado, LibroRemote? libro, string? errorMessage)> GetLibro(Guid libroId);
}

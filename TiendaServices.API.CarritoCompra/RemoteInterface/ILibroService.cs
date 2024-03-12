using TiendaServices.API.CarritoCompra.RemoteModel;

namespace TiendaServices.API.CarritoCompra.RemoteInterface;

public interface ILibroService
{
    Task<(bool resultado, LibroRemote? libro, string? errorMessage)> GetLibro(Guid libroId);
}

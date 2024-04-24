using TiendaServicios.Api.Gateway.LibroRemote;

namespace TiendaServicios.Api.Gateway.InterfaceRemote;

public interface IAutorRemote
{
    public Task<(bool resultado, AutorModeloRemote? autor, string messageError)> GetAutor(Guid autorId);
}

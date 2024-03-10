using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaService.API.Autor.Models;
using TiendaService.API.Autor.Persistence;

namespace TiendaService.API.Autor.Application;

public class Consulta
{
    public class ListaAutor : IRequest<List<AutorLibro>> { }
    public class Manejado : IRequestHandler<ListaAutor, List<AutorLibro>>
    {
        private readonly ContextAutor _contextAutor;
        public Manejado(ContextAutor contextAutor)
        {
            _contextAutor = contextAutor;
        }
        public async Task<List<AutorLibro>> Handle(ListaAutor request, CancellationToken cancellationToken)
        {
            var autores = await _contextAutor.AutorLibros.ToListAsync(cancellationToken);
            return autores;
        }
    }

}

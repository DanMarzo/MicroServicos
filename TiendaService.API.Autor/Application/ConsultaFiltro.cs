using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaService.API.Autor.Models;
using TiendaService.API.Autor.Persistence;

namespace TiendaService.API.Autor.Application;

public class ConsultaFiltro
{
    public class AutorUnico : IRequest<AutorLibro>
    {
        public string? AutorGuid { get; set; }
    }
    public class Manejador : IRequestHandler<AutorUnico, AutorLibro>
    {
        private readonly ContextAutor _contextAutor;
        public Manejador(ContextAutor contextAutor)
        {
            _contextAutor = contextAutor;
        }
        public async Task<AutorLibro> Handle(AutorUnico request, CancellationToken cancellationToken)
        {
            var autorLibro = await _contextAutor.AutorLibros
                .Where(
                    x => x.AutorLibroGuid == request.AutorGuid
                    ).FirstOrDefaultAsync();
            if (autorLibro is null) throw new Exception("Autor não encontrado");
            return autorLibro;
        }
    }
}
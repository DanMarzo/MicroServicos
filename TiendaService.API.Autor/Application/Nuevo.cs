using MediatR;
using TiendaService.API.Autor.Models;
using TiendaService.API.Autor.Persistence;

namespace TiendaService.API.Autor.Application;

public class Nuevo
{
    public class Ejecuta : IRequest<AutorLibro>
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }
    }
    public class Manejador : IRequestHandler<Ejecuta, AutorLibro>
    {
        private readonly ContextAutor _contextAutor;
        public Manejador(ContextAutor contextAutor)
        {
            _contextAutor = contextAutor;
        }

        public async Task<AutorLibro> Handle(Ejecuta request, CancellationToken cancellationToken)
        {
            AutorLibro autorLibro = new()
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                FechaNacimiento = request.FechaNacimiento,
                AutorLibroGuid = Guid.NewGuid().ToString()
            };
            await _contextAutor.AddAsync(autorLibro);
            var linhasAlteradas = await _contextAutor.SaveChangesAsync();

            if (linhasAlteradas <= 0)
                throw new Exception("Nao foi possivel concluir a alteracao");
            return autorLibro;
        }
    }
}
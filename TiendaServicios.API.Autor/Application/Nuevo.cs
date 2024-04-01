using FluentValidation;
using MediatR;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;
using TiendaServicios.API.Autor.Models;
using TiendaServicios.API.Autor.Persistence;

namespace TiendaServicios.API.Autor.Application;

public class Nuevo
{
    public class Ejecuta : IRequest<AutorLibro>
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }
    }
    public class EjecutaValidation : AbstractValidator<Ejecuta>
    {
        public EjecutaValidation()
        {
            RuleFor(x => x.Nombre).NotEmpty();
            RuleFor(x => x.Apellido).NotEmpty();
        }
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
using FluentValidation;
using MediatR;
using TiendaServicios.API.Libro.Models;
using TiendaServicios.API.Libro.Persistence;
using TiendaServicios.RabbitMQ.Bus.BusRabbit;
using TiendaServicios.RabbitMQ.Bus.EventoQue;

namespace TiendaServicios.API.Libro.Application;

public class Nuevo
{
    public class Ejecuta : IRequest<LibreriaMaterial>
    {
        public string? Titulo { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public Guid? AutorLibro { get; set; }
    }
    public class EjecutaValidacion : AbstractValidator<Ejecuta>
    {
        public EjecutaValidacion()
        {
            RuleFor(x => x.Titulo).NotEmpty();
            RuleFor(x => x.FechaPublicacion).NotEmpty();
            RuleFor(x => x.AutorLibro).NotEmpty();
        }
    }


    public class Manejador : IRequestHandler<Ejecuta, LibreriaMaterial>
    {
        private readonly ContextLibreria _contextLibreria;
        private readonly IRabbitEventBus _rabbitEventBus;

        public Manejador(
            ContextLibreria contextLibreria,
            IRabbitEventBus rabbitEventBus)
        {
            _contextLibreria = contextLibreria;
            _rabbitEventBus = rabbitEventBus;
        }

        public async Task<LibreriaMaterial> Handle(Ejecuta request, CancellationToken cancellationToken)
        {
            var libreria = new LibreriaMaterial
            {
                AutorLibro = request.AutorLibro,
                Titulo = request.Titulo,
                FechaPublicacion = request.FechaPublicacion,
            };

            await _contextLibreria.AddAsync(libreria);
            int linhas = await _contextLibreria.SaveChangesAsync();
            _rabbitEventBus.Publish(new EmailEventoQue("marzogildan15@gmail.com", libreria.Titulo, "conteudo zoado"));

            if (linhas <= 0)
                throw new Exception("Nao foi possivel salvar as informacoes");
            return libreria;
        }
    }
}

using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using TiendaServicios.RabbitMQ.Bus.BusRabbit;
using TiendaServicios.RabbitMQ.Bus.Comandos;
using TiendaServicios.RabbitMQ.Bus.Eventos;

namespace TiendaServicios.RabbitMQ.Bus.Implement;

public class RabbitEventBus : IRabbitEventBus
{
    private readonly IMediator _mediator;
    private readonly Dictionary<string, List<Type>> _manejadores;
    private readonly List<Type> _eventosTipos;

    public RabbitEventBus(IMediator mediator)
    {
        _mediator = mediator;
        _manejadores = new Dictionary<string, List<Type>>();
        _eventosTipos = new List<Type>();
    }

    public async Task EnviarComando<T>(T comando) where T : Comando
    {
        _mediator.Send(comando);
    }

    public void Publish<T>(T evento) where T : Evento
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        var eventName = evento.GetType().Name;

        channel.QueueDeclare(eventName, false,false, false, null);

        var message = JsonSerializer.Serialize(evento);
        var body = Encoding.UTF8.GetBytes(message);
        channel.BasicPublish("", eventName, null, body);
    }

    public void Subscribe<T, TH>()
        where T : Evento
        where TH : IEventoManejador
    {
        throw new NotImplementedException();
    }
}

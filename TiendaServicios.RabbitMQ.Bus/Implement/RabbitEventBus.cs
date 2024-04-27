using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
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
        var factory = new ConnectionFactory() { HostName = "rabbitmq" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        var eventName = evento.GetType().Name;

        channel.QueueDeclare(eventName, false, false, false, null);

        var message = JsonSerializer.Serialize(evento);
        var body = Encoding.UTF8.GetBytes(message);
        channel.BasicPublish("", eventName, null, body);
    }

    public void Subscribe<T, TH>()
        where T : Evento
        where TH : IEventoManejador<T>
    {
        var eventName = typeof(T).Name;
        var manejadoTipo = typeof(TH);

        if (!_eventosTipos.Contains(typeof(T)))
        {
            _eventosTipos.Add(typeof(T));
        }
        if (!_manejadores.ContainsKey(eventName))
        {
            _manejadores.Add(eventName, new List<Type>());
        }
        if (_manejadores[eventName].Any(x => x.GetType() == manejadoTipo))
        {
            throw new Exception($"El manejandor {manejadoTipo.Name}, fue registrado por {eventName}");
        }

        _manejadores[eventName].Add(manejadoTipo);

        var factory = new ConnectionFactory()
        {
            HostName = "rabbitmq",
            DispatchConsumersAsync = true,
        };
        using var connection = factory.CreateConnection();
        using var chanel = connection.CreateModel();
        chanel.QueueDeclare(eventName, false, false, false, null);
        var consumer = new AsyncEventingBasicConsumer(chanel);

        consumer.Received += Consumer_Delegate; //Encarregado de ler as mensagens do QUE

        chanel.BasicConsume(eventName, true, consumer);

    }
    //Encarregado de ler as mensagens do QUE
    private async Task Consumer_Delegate(object sender, BasicDeliverEventArgs @event)
    {
        var eventName = @event.RoutingKey;
        var message = Encoding.UTF8.GetString(@event.Body.ToArray());
        try
        {
            if (_manejadores.ContainsKey(eventName))
            {
                var subscriptions = _manejadores[eventName];
                foreach (var subscription in subscriptions)
                {
                    var manejador = Activator.CreateInstance(subscription);
                    if (manejador == null) continue;
                    var tipoEvento = _eventosTipos.SingleOrDefault(x => x.Name == eventName);
                    var eventDs = JsonSerializer.Deserialize("", tipoEvento);
                    var concretoTipo = typeof(IEventoManejador<>).MakeGenericType(tipoEvento);
                    await (Task)concretoTipo.GetMethod("Handle").Invoke(manejador, new object[] { eventDs });

                }
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}

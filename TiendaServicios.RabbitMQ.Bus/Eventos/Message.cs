using MediatR;

namespace TiendaServicios.RabbitMQ.Bus.Eventos;

public abstract class Message : IRequest<bool>
{
    public string MessageType { get; protected set; }
    protected Message()
    {
        this.MessageType = GetType().Name;
    }
}

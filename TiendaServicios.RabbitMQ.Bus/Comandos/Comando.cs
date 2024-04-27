using TiendaServicios.RabbitMQ.Bus.Eventos;

namespace TiendaServicios.RabbitMQ.Bus.Comandos;

public abstract class Comando : Message
{
    public DateTime TimeStamp { get; protected set; }
    protected Comando()
    {
        this.TimeStamp = DateTime.Now;
    }
}

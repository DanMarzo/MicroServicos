namespace TiendaServicios.RabbitMQ.Bus.Eventos;

public abstract class Evento
{
    public DateTime TimeStamp { get; set; }
    protected Evento()
    {
        this.TimeStamp = DateTime.Now;
    }
}

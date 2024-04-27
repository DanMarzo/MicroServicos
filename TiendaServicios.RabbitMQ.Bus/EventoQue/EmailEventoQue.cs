using TiendaServicios.RabbitMQ.Bus.Eventos;

namespace TiendaServicios.RabbitMQ.Bus.EventoQue;

public class EmailEventoQue : Evento
{
    public string Destinatario { get; set; }
    public string Titulo { get; set; }
    public string Contenido { get; set; }

    public EmailEventoQue(string destinatario, string titulo, string contenido)
    {
        Destinatario = destinatario;
        Titulo = titulo;
        Contenido = contenido;
    }

}

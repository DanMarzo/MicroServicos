using AutoMapper;
using Moq;
using TiendaServico.API.Libro.Application;
using TiendaServico.API.Libro.Persistence;

namespace TiendaServico.API.Libro.Test;

public class LibroServiceTest
{


    [Fact]
    public void GetLibros()
    {
        var moqContext = new Mock<ContextLibreria>();
        var moqMapper = new Mock<IMapper>();
        Consulta.Manejador manejador = new Consulta.Manejador(moqContext.Object, moqMapper.Object);
    }
}

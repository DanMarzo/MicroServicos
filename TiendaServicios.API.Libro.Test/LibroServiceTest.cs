using AutoMapper;
using Moq;
using TiendaServico.API.Libro.Application.DTOs;
using GenFu;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TiendaServicios.API.Libro.Persistence;
using TiendaServicios.API.Libro.Application;
using TiendaServicios.API.Libro.Models;
using TiendaServico.API.Libro.Test;

namespace TiendaServicios.API.Libro.Test;

public class LibroServiceTest
{
    private IEnumerable<LibreriaMaterial> ObtenerDataPrueba()
    {
        GenFu.GenFu.Configure<LibreriaMaterial>()
            .Fill(x => x.Titulo).AsArticleTitle()
            .Fill(x => x.LibreriaMaterialId, () => Guid.NewGuid());
        var lista = GenFu.GenFu.ListOf<LibreriaMaterial>(30);
        lista[0].LibreriaMaterialId = Guid.Empty;
        return lista;
    }

    private Mock<ContextLibreria> CrearContexto()
    {
        var dataPrueba = ObtenerDataPrueba().AsQueryable();
        var dbSet = new Mock<DbSet<LibreriaMaterial>>();
        //Definindo propriedades para que a classe dbSet possa usar as configuracoes do EF Core, uma vez que eu nao uso o SQLServer ou diretamente o DB eu preciso configurar isso manualmente

        dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(dataPrueba.Provider);
        dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Expression).Returns(dataPrueba.Expression);
        dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.ElementType).Returns(dataPrueba.ElementType);
        dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.GetEnumerator()).Returns(dataPrueba.GetEnumerator());
        //Propriedade para configurar ASYNC
        dbSet.As<IAsyncEnumerable<LibreriaMaterial>>().Setup(x => x.GetAsyncEnumerator(new CancellationToken()))
            .Returns(new AsyncEnumerator<LibreriaMaterial>(dataPrueba.GetEnumerator()));

        dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(new AsyncQueryProvider<LibreriaMaterial>(dataPrueba.Provider));

        var contexto = new Mock<ContextLibreria>();
        contexto.Setup(x => x.LibreriasMaterials).Returns(dbSet.Object);
        return contexto;
    }

    [Fact]
    public async void GetLibroId()
    {
        var mockContext = CrearContexto();
        var mapConfig = new MapperConfiguration(x => x.AddProfile(new MappingTest()));
        var mapper = mapConfig.CreateMapper();

        var request = new ConsultaFiltro.LibroUnico();
        request.LibroId = Guid.Empty;
        var manejador = new ConsultaFiltro.Manejador(mockContext.Object, mapper);

        var libre = await manejador.Handle(request, new CancellationToken());
        Assert.NotNull(libre);
        Assert.True(libre.LibreriaMaterialId == Guid.Empty);
    }


    [Fact]
    public async Task GetLibrosAsync()
    {

        var moqContext = CrearContexto();
        var mapConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingTest()));
        var mapper = mapConfig.CreateMapper();
        Consulta.Manejador manejador = new Consulta.Manejador(moqContext.Object, mapper);

        Consulta.Ejecuta request = new Consulta.Ejecuta();

        var lista = await manejador.Handle(request, new CancellationToken());
        Assert.True(lista.Any());
    }

    [Fact]
    public async void GuardarLibro()
    {
        //Recomendavel usar somente em um metodo
        Debugger.Launch();
        var options = new DbContextOptionsBuilder<ContextLibreria>().UseInMemoryDatabase(databaseName: "BaseDatos").Options;
        var contexto = new ContextLibreria(options);

        var request = new Nuevo.Ejecuta();
        request.Titulo = "Livro do sofrimento com testes unitários";
        request.AutorLibro = Guid.Empty;
        request.FechaPublicacion = DateTime.Now;
        var manejador = new Nuevo.Manejador(contexto);
        var libreria = await manejador.Handle(request, new CancellationToken());
        Assert.True(libreria is not null);
    }
}

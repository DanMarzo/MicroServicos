using AutoMapper;
using Moq;
using TiendaServico.API.Libro.Application;
using TiendaServico.API.Libro.Application.DTOs;
using TiendaServico.API.Libro.Persistence;
using GenFu;
using TiendaServico.API.Libro.Models;
using Microsoft.EntityFrameworkCore;

namespace TiendaServico.API.Libro.Test;

public class LibroServiceTest
{
    private IEnumerable<LibreriaMaterial> obtenerDataPrueba()
    {
        A.Configure<LibreriaMaterial>()
            .Fill(x => x.Titulo).AsArticleTitle()
            .Fill(x => x.LibreriaMaterialId, () => Guid.NewGuid());
        var lista = A.ListOf<LibreriaMaterial>(30);
        lista[0].LibreriaMaterialId = Guid.Empty;
        return lista;
    }

    private Mock<ContextLibreria> CrearContexto()
    {
        var dataPrueba = obtenerDataPrueba().AsQueryable();
        var dbSet = new Mock<DbSet<LibreriaMaterial>>();
        //Definindo propriedades para que a classe dbSet possa usar as configuracoes do EF Core, uma vez que eu nao uso o SQLServer ou diretamente o DB eu preciso configurar isso manualmente

        dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(dataPrueba.Provider);
        dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Expression).Returns(dataPrueba.Expression);
        dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.ElementType).Returns(dataPrueba.ElementType);
        dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.GetEnumerator()).Returns(dataPrueba.GetEnumerator());
        //Propriedade para configurar ASYNC
        dbSet.As<IAsyncEnumerable<LibreriaMaterial>>().Setup(x => x.GetAsyncEnumerator(new CancellationToken()))
            .Returns(new AsyncEnumerator<LibreriaMaterial>(dataPrueba.GetEnumerator()));
        var contexto = new Mock<ContextLibreria>();
        contexto.Setup(x => x.LibreriasMaterials).Returns(dbSet.Object);
        return contexto;
    }

    [Fact]
    public void GetLibros()
    {
        var moqContext = CrearContexto();
        var mapConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingTest()));
        var mapper = mapConfig.CreateMapper();
        Consulta.Manejador manejador = new Consulta.Manejador(moqContext.Object, mapper);
    }
}

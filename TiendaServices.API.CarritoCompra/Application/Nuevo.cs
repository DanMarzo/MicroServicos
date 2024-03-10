using MediatR;
using Swashbuckle.AspNetCore.SwaggerGen;
using TiendaServices.API.CarritoCompra.Models;
using TiendaServices.API.CarritoCompra.Persistence;

namespace TiendaServices.API.CarritoCompra.Application;

public class Nuevo
{
    public class Ejecuta : IRequest<CarritoSession>
    {
        public required DateTime FechaCreacionSession { get; set; }
        public List<string>? ProductoLista { get; set; }
    }
    public class Manejador : IRequestHandler<Ejecuta, CarritoSession>
    {
        private readonly ContextCarrito _contextCarrito;
        public Manejador(ContextCarrito contextCarrito)
        {
            _contextCarrito = contextCarrito;
        }
        public async Task<CarritoSession> Handle(Ejecuta request, CancellationToken cancellationToken)
        {
            var carritoSession = new CarritoSession
            {
                FechaCreacion = request.FechaCreacionSession,
            };
            await _contextCarrito.AddAsync(carritoSession);
            var linhas = await _contextCarrito.SaveChangesAsync();
            if (linhas <= 0) throw new Exception("Nao foi possivel salvar o Carrinho");

            foreach (var item in request.ProductoLista)
            {
                var detalhesSession = new CarritoSessionDetalle
                {
                    FechaOperacion = DateTime.Now,
                    CarritoSessionId = (int)carritoSession.CarritoSessionId!,
                    ProductoSelecioado = item
                };
                await _contextCarrito.AddAsync(detalhesSession);
            }
            var linhasdetalhesSession = await _contextCarrito.SaveChangesAsync(cancellationToken);
            if (linhasdetalhesSession <= 0)
                throw new Exception("Nao foi possivel incluir os detalhes do carrinhos de compras");
            return carritoSession;
        }
    }
}


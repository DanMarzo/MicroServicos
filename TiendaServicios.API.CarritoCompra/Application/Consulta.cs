using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.API.CarritoCompra.Application.DTOs;
using TiendaServicios.API.CarritoCompra.Persistence;
using TiendaServicios.API.CarritoCompra.RemoteInterface;

namespace TiendaServicios.API.CarritoCompra.Application;

public class Consulta
{
    public class Ejecuta : IRequest<CarritoDTO>
    {
        public int CarritoSessionId { get; set; }
    }
    public class Manejador : IRequestHandler<Ejecuta, CarritoDTO>
    {
        private readonly ContextCarrito _carritoContext;
        private readonly ILibroService _libroService;
        public Manejador(ContextCarrito carritoContext, ILibroService libroService)
        {
            _carritoContext = carritoContext; _libroService = libroService;
        }
        public async Task<CarritoDTO> Handle(Ejecuta request, CancellationToken cancellationToken)
        {
            var carritoSession =
                await _carritoContext.CarritoSession.FirstOrDefaultAsync(
                    x => x.CarritoSessionId == request.CarritoSessionId
                    );
            var carritoSessionDetalle =
                await _carritoContext.CarritoSessionDetalle
                .Where(x => x.CarritoSessionId == request.CarritoSessionId).ToListAsync();
            List<CarritoDetalleDTO> listaCarritoDto = [];

            foreach (var libro in carritoSessionDetalle)
            {
                var response = await _libroService.GetLibro(Guid.Parse(libro.ProductoSelecioado));
                if (response.resultado)
                {
                    var objetoLibro = response.libro;
                    var carritoDetalhe = new CarritoDetalleDTO
                    {
                        AutorLibro = objetoLibro.AutorLibro.ToString(),
                        LibroId = objetoLibro.LibreriaMaterialId,
                        FechaPublicacion = objetoLibro.FechaPublicacion,
                        TituloLibro = objetoLibro.Titulo
                    };
                    listaCarritoDto.Add(carritoDetalhe);
                }
            }
            var carritoSessionDTO = new CarritoDTO
            {
                CarritoId = (int)carritoSession.CarritoSessionId,
                FechaCreacionSession = carritoSession.FechaCreacion,
                ListaProductos = listaCarritoDto
            };
            return carritoSessionDTO;
        }
    }
}

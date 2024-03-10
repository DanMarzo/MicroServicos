using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaService.API.Autor.Models;
using TiendaService.API.Autor.Persistence;

namespace TiendaService.API.Autor.Application;

public class Consulta
{
    public class ListaAutor : IRequest<List<AutorDTO>> { }
    public class Manejador : IRequestHandler<ListaAutor, List<AutorDTO>>
    {
        private readonly ContextAutor _contextAutor;
        private readonly IMapper _mapper;
        public Manejador(ContextAutor contextAutor, IMapper mapper)
        {
            _contextAutor = contextAutor;
            _mapper = mapper;
        }
        public async Task<List<AutorDTO>> Handle(ListaAutor request, CancellationToken cancellationToken)
        {
            var autores = await _contextAutor.AutorLibros.ToListAsync(cancellationToken);
            var autoresDTO = _mapper.Map<List<AutorLibro>, List<AutorDTO>>(autores);
            return autoresDTO;
        }
    }

}

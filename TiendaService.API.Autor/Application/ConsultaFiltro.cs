using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaService.API.Autor.Models;
using TiendaService.API.Autor.Persistence;

namespace TiendaService.API.Autor.Application;

public class ConsultaFiltro
{
    public class AutorUnico : IRequest<AutorDTO>
    {
        public string? AutorGuid { get; set; }
    }
    public class Manejador : IRequestHandler<AutorUnico, AutorDTO>
    {
        private readonly ContextAutor _contextAutor;
        private readonly IMapper _mapper;
        public Manejador(ContextAutor contextAutor, IMapper mapper)
        {
            _contextAutor = contextAutor;
            _mapper = mapper;
        }
        public async Task<AutorDTO> Handle(AutorUnico request, CancellationToken cancellationToken)
        {
            var autorLibro = await _contextAutor.AutorLibros
                .Where(
                    x => x.AutorLibroGuid == request.AutorGuid
                    ).FirstOrDefaultAsync();
            if (autorLibro is null) throw new Exception("Autor não encontrado");
            var autorDto = _mapper.Map<AutorDTO>(autorLibro);
            return autorDto;
        }
    }
}
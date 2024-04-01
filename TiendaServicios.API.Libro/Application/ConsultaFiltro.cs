using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.API.Libro.Application.DTOs;
using TiendaServicios.API.Libro.Models;
using TiendaServicios.API.Libro.Persistence;

namespace TiendaServicios.API.Libro.Application;

public class ConsultaFiltro
{
    public class LibroUnico : IRequest<LibroMaterialDTO>
    {
        public Guid? LibroId { get; set; }
    }

    public class Manejador : IRequestHandler<LibroUnico, LibroMaterialDTO>
    {
        private readonly ContextLibreria _contextLibreria;
        private readonly IMapper _mapper;
        public Manejador(ContextLibreria contextLibreria, IMapper mapper)
        {
            _contextLibreria = contextLibreria;
            _mapper = mapper;
        }
        public async Task<LibroMaterialDTO> Handle(LibroUnico request, CancellationToken cancellationToken)
        {
            LibreriaMaterial? libreria = await _contextLibreria.LibreriasMaterials
                .Where(x => x.LibreriaMaterialId == request.LibroId).FirstOrDefaultAsync();
            if (libreria is null)
                throw new Exception("Livro nao foi encontrado!");
            var libreriaDTO = _mapper.Map<LibroMaterialDTO>(libreria);
            return libreriaDTO;
        }
    }

}

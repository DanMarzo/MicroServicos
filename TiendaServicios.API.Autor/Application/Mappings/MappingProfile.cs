using AutoMapper;
using TiendaServicios.API.Autor.Application.DTOs;
using TiendaServicios.API.Autor.Models;

namespace TiendaServicios.API.Autor.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AutorLibro, AutorDTO>().ReverseMap();
    }
}

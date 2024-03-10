using AutoMapper;
using TiendaService.API.Autor.Application.DTOs;
using TiendaService.API.Autor.Models;

namespace TiendaService.API.Autor.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AutorLibro, AutorDTO>().ReverseMap();
    }
}

using AutoMapper;
using TiendaService.API.Autor.Models;

namespace TiendaService.API.Autor.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AutorLibro, AutorDTO>().ReverseMap();
    }
}

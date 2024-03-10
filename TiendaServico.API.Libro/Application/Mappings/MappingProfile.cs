using AutoMapper;
using TiendaServico.API.Libro.Application.DTOs;
using TiendaServico.API.Libro.Models;

namespace TiendaServico.API.Libro.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<LibreriaMaterial, LibroMaterialDTO>().ReverseMap();
    }
}

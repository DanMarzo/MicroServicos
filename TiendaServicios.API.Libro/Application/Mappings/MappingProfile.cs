using AutoMapper;
using TiendaServicios.API.Libro.Application.DTOs;
using TiendaServicios.API.Libro.Models;

namespace TiendaServicios.API.Libro.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<LibreriaMaterial, LibroMaterialDTO>().ReverseMap();
    }
}

using AutoMapper;
using TiendaServicios.API.Libro.Application.DTOs;
using TiendaServicios.API.Libro.Models;

namespace TiendaServicios.API.Libro.Test;

public class MappingTest : Profile
{
    public MappingTest()
    {
        CreateMap<LibreriaMaterial, LibroMaterialDTO>().ReverseMap();
    }
}

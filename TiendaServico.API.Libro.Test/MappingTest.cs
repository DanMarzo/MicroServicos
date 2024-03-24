using AutoMapper;
using TiendaServico.API.Libro.Application.DTOs;
using TiendaServico.API.Libro.Models;

namespace TiendaServico.API.Libro.Test;

public class MappingTest : Profile
{
    public MappingTest()
    {
        CreateMap<LibreriaMaterial, LibroMaterialDTO>().ReverseMap();
    }
}

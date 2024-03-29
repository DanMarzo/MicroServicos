﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServico.API.Libro.Application.DTOs;
using TiendaServico.API.Libro.Persistence;

namespace TiendaServico.API.Libro.Application;

public class Consulta
{
    public class Ejecuta : IRequest<List<LibroMaterialDTO>> { }
    public class Manejador : IRequestHandler<Ejecuta, List<LibroMaterialDTO>>
    {
        private readonly ContextLibreria _contextLibreria;
        private readonly IMapper _mapper;
        public Manejador(ContextLibreria contextLibreria, IMapper mapper)
        {
            _contextLibreria = contextLibreria;
            _mapper = mapper;
        }
        public async Task<List<LibroMaterialDTO>> Handle(Ejecuta request, CancellationToken cancellationToken)
        {
            var librerias = await _contextLibreria.LibreriasMaterials.ToListAsync(cancellationToken);
            var libreriasDTOs = _mapper.Map<List<LibroMaterialDTO>>(librerias);
            return libreriasDTOs;
        }
    }
}

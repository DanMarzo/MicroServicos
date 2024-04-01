using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.API.Libro.Application;
using TiendaServicios.API.Libro.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>()
    );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ContextLibreria>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionSQLServer")));
builder.Services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(Nuevo.Manejador).Assembly));
builder.Services.AddAutoMapper(typeof(Consulta.Ejecuta));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

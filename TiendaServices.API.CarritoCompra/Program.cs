using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TiendaServices.API.CarritoCompra.Application;
using TiendaServices.API.CarritoCompra.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ContextCarrito>(x => x.UseMySQL(builder.Configuration.GetConnectionString("ConnectionMySQL")!));
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining<Nuevo.Ejecuta>());
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

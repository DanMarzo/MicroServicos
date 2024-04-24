using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using TiendaServicios.Api.Gateway.ImplementRemote;
using TiendaServicios.Api.Gateway.InterfaceRemote;
using TiendaServicios.Api.Gateway.MessageHandler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient("AutorService", x =>
{
    x.BaseAddress = new Uri(builder.Configuration["Services:Autor"]);
});

builder.Services.AddScoped<IAutorRemote, AutorRemote>();
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("Policy", pt =>
    {
        pt.AllowAnyMethod();
        pt.AllowAnyHeader();
        pt.SetIsOriginAllowedToAllowWildcardSubdomains();
    });
});

builder.Configuration.AddJsonFile("ocelot.json");
builder.Services.AddOcelot()
    .AddDelegatingHandler<LivroHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseAuthorization();
app.UseRouting();

app.MapControllers();
app.UseCors("CorsPolicy");

app.UseOcelot().Wait();

app.Run();

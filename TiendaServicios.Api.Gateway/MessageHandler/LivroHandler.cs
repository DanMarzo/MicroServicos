using System.Diagnostics;
using System.Text;
using System.Text.Json;
using TiendaServicios.Api.Gateway.InterfaceRemote;
using TiendaServicios.Api.Gateway.LibroRemote;

namespace TiendaServicios.Api.Gateway.MessageHandler;

public class LivroHandler : DelegatingHandler
{
    private readonly ILogger<LivroHandler> _logger;
    private readonly IAutorRemote _autorRemote;

    public LivroHandler(ILogger<LivroHandler> logger, IAutorRemote autorRemote)
    {
        _logger = logger;
        _autorRemote = autorRemote;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var tiempo = Stopwatch.StartNew();
        _logger.LogInformation("Inicio do request");
        var response = await base.SendAsync(request, cancellationToken);


        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var result = JsonSerializer.Deserialize<LibroModelRemote>(content, option);
            var responseAutor  = await _autorRemote.GetAutor(result?.AutorLibro ?? Guid.Empty);
            if (responseAutor.resultado)
            {
                result.AutorModeloRemote = responseAutor.autor;
                response.Content = new StringContent(JsonSerializer.Serialize(responseAutor), Encoding.UTF8, "application/json");
                return response;
            }
        }
        _logger.LogInformation($"El processo se hizo en {tiempo.ElapsedMilliseconds}ms");
        return response;
    }
}


using System.Diagnostics;
using System.Text.Json;
using TiendaServicios.Api.Gateway.LibroRemote;

namespace TiendaServicios.Api.Gateway.MessageHandler;

public class LivroHandler : DelegatingHandler
{
    private readonly ILogger<LivroHandler> _logger;

    public LivroHandler(ILogger<LivroHandler> logger)
    {
        _logger = logger;
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

        }

        _logger.LogInformation($"El processo se hizo en {tiempo.ElapsedMilliseconds}ms");
        return response;
    }
}


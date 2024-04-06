using System.Diagnostics;

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
        _logger.LogInformation($"El processo se hizo en {tiempo.ElapsedMilliseconds}ms");
        return response;
    }
}

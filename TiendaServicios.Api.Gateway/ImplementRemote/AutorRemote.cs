using Microsoft.Extensions.Logging;
using System.Text.Json;
using TiendaServicios.Api.Gateway.InterfaceRemote;
using TiendaServicios.Api.Gateway.LibroRemote;

namespace TiendaServicios.Api.Gateway.ImplementRemote;

public class AutorRemote : IAutorRemote
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<AutorRemote> _logger;

    public AutorRemote(IHttpClientFactory httpClientFactory, ILogger<AutorRemote> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<(bool resultado, AutorModeloRemote? autor, string messageError)> GetAutor(Guid autorId)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("AutorService");
            var response = await client.GetAsync($"Autor/{autorId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var autor = JsonSerializer.Deserialize<AutorModeloRemote>(content, options);
                return (true, autor!, string.Empty);
            }
            return (false, null, "Houve erros ao executar");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return (false, null, ex.Message);
        }
    }
}

using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.Extensions.Logging;
using MySqlX.XDevAPI.Common;
using System.Text.Json;
using TiendaServices.API.CarritoCompra.Models.Consts;
using TiendaServices.API.CarritoCompra.RemoteInterface;
using TiendaServices.API.CarritoCompra.RemoteModel;

namespace TiendaServices.API.CarritoCompra.RemoteService;

public class LibroService : ILibroService
{
    #region DIs
    private readonly ILogger<LibroService> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public LibroService(
        ILogger<LibroService> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }
    #endregion
    private const string uriBase = "api/LibroMaterial";
    public async Task<(bool resultado, LibroRemote? libro, string? errorMessage)> GetLibro(Guid libroId)
    {
        try
        {
            using var cliente = _httpClientFactory.CreateClient(HttpConsts.LibrosService);
            using var response = await cliente.GetAsync($"{uriBase}/GetLibroUnico?id={libroId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<LibroRemote>(content);
                return (true, result, null);
            }
            return (false, null, null);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return (false, null, ex.Message);
        }
    }
}

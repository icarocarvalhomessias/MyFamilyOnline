using Familia.WebApp.MVC.Extensions;
using Familia.WebApp.MVC.Models;
using Familia.WebApp.MVC.Services;
using FML.WebApp.MVC.Services.Interface;
using Microsoft.Extensions.Options;

namespace FML.WebApp.MVC.Services
{
    public class EventoService : Service, IEventoService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<EventoService> _logger;

        public EventoService(HttpClient httpClient,
                                   IOptions<AppSettings> settings,
                                   ILogger<EventoService> logger)
        {

            if (settings?.Value?.EventoUrl == null)
            {
                throw new ArgumentNullException(nameof(settings.Value.EventoUrl), "EventoUrl cannot be null.");
            }

            _logger = logger;
            _logger.LogInformation("EventoUrl: {EventoUrl}", settings.Value.EventoUrl);

            // Bypass SSL certificate validation (for development purposes only)
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(settings.Value.EventoUrl)
            };
        }

        public async Task<List<SecretSantaPair>> RealizaAmigoOculto()
        {
            var response = await _httpClient.GetAsync("/api/eventos");
            var teste = await response.Content.ReadAsStringAsync();

            try
            {
                if (!TratarErrosResponse(response))
                {
                    return await DeserializarObjetoResponse<List<SecretSantaPair>>(response);
                }
            }
            catch
            {
                return new List<SecretSantaPair>();
            }

            return await DeserializarObjetoResponse<List<SecretSantaPair>>(response);
        }

        public async Task<List<SecretSantaPair>> RefazAmigoOculto()
        {
            var response = await _httpClient.GetAsync("/api/eventos/refaz-amigo-oculto");
            var teste = await response.Content.ReadAsStringAsync();

            try
            {
                if (!TratarErrosResponse(response))
                {
                    return await DeserializarObjetoResponse<List<SecretSantaPair>>(response);
                }
            }
            catch
            {
                return new List<SecretSantaPair>();
            }

            return await DeserializarObjetoResponse<List<SecretSantaPair>>(response);
        }

        public async Task<List<Parente>> GetParentes()
        {
            var response = await _httpClient.GetAsync("/api/eventos/parentes");

            try
            {
                if (!TratarErrosResponse(response))
                {
                    return await DeserializarObjetoResponse<List<Parente>>(response);
                }
            }
            catch
            {
                return new List<Parente>();
            }

            return await DeserializarObjetoResponse<List<Parente>>(response);
        }
    }
}

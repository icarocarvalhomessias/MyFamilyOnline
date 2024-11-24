using Familia.WebApp.MVC.Extensions;
using Familia.WebApp.MVC.Models;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace FML.WebApp.MVC.Clients.HttpServices
{
    public class FamiliaHttpService : HttpService, IFamiliaHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<FamiliaHttpService> _logger;

        public FamiliaHttpService(HttpClient httpClient,
                                   IOptions<AppSettings> settings,
                                   ILogger<FamiliaHttpService> logger)
        {
            if (settings?.Value?.FamiliaUrl == null)
            {
                throw new ArgumentNullException(nameof(settings.Value.FamiliaUrl), "FamiliaUrl cannot be null.");
            }

            _logger = logger;
            _logger.LogInformation("FamiliaUrl: {FamiliaUrl}", settings.Value.FamiliaUrl);

            // Bypass SSL certificate validation (for development purposes only)
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(settings.Value.FamiliaUrl)
            };
        }

        public async Task<List<Relative>> GetRelativeByFamilyId(Guid familyId)
        {

            var endpoint = $"/family/{familyId}";
            var response = await _httpClient.GetAsync(endpoint);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Error fetching relatives for family ID {FamilyId}: {StatusCode}", familyId, response.StatusCode);
                response.EnsureSuccessStatusCode();
            }

            var relatives = await response.Content.ReadFromJsonAsync<List<Relative>>();
            return relatives ?? new List<Relative>();
        }


    }
}

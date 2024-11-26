using Familia.WebApp.MVC.Extensions;
using FML.WebApp.MVC.Clients.HttpServices.Interface;
using Microsoft.Extensions.Options;
using System.Net.Http;

namespace FML.WebApp.MVC.Clients.HttpServices
{
    public class FamiliaHttpService : HttpService, IFamiliaHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<FamiliaHttpService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public FamiliaHttpService(HttpClient httpClient,
                                   IOptions<AppSettings> settings,
                                   ILogger<FamiliaHttpService> logger,
                                   IHttpClientFactory httpClientFactory)
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
            _httpClientFactory = httpClientFactory;

            _httpClient = _httpClientFactory.CreateClient("FamiliaresAPI");
        }

        public async Task<List<Relative>> GetRelativeByFamilyId(Guid familyId)
        {
            var endpoint = $"/family/{familyId}";
            var response = await _httpClient.GetAsync(endpoint);

            try
            {
                if (!TratarErrosResponse(response))
                {
                    return await DeserializarObjetoResponse<List<Relative>>(response);
                }
            }
            catch
            {
                return new List<Relative>();
            }

            return await DeserializarObjetoResponse<List<Relative>>(response);
        }

        public async Task<Relative> GetRelativeById(Guid relativeId)
        {
            var endpoint = $"/api/relative/{relativeId}";
            var response = await _httpClient.GetAsync(endpoint);

            try
            {
                if (!TratarErrosResponse(response))
                {
                    return await DeserializarObjetoResponse<Relative>(response);
                }
            }
            catch
            {
                return null;
            }

            return await DeserializarObjetoResponse<Relative>(response);
        }

        public async Task<List<Family>> GetFamilies()
        {
            var endpoint = "/families";
            var response = await _httpClient.GetAsync(endpoint);

            try
            {
                if (!TratarErrosResponse(response))
                {
                    return await DeserializarObjetoResponse<List<Family>>(response);
                }
            }
            catch
            {
                return new List<Family>();
            }

            return await DeserializarObjetoResponse<List<Family>>(response);
        }

        public async Task<List<House>> GetHousesByFamilyId(Guid familyId)
        {
            var endpoint = $"/house/family/{familyId}";
            var response = await _httpClient.GetAsync(endpoint);

            try
            {
                if (!TratarErrosResponse(response))
                {
                    return await DeserializarObjetoResponse<List<House>>(response);
                }
            }
            catch
            {
                return new List<House>();
            }

            return await DeserializarObjetoResponse<List<House>>(response);
        }

        public async Task AddRelative(Relative relative)
        {
            var endpoint = $"/api/relative/";
            var content = ObterConteudo(relative);
            var response = await _httpClient.PostAsync(endpoint, content);

            if (!TratarErrosResponse(response))
            {
                _logger.LogError("Error adding relative {Relative}: {StatusCode}", relative, response.StatusCode);
            }
        }

        public async Task UpdateRelative(Relative relative)
        {
            var endpoint = "/api/relative/";
            var content = ObterConteudo(relative);

            _logger.LogInformation("Sending PUT request to {Endpoint} with content: {Content}", endpoint, content);

            var response = await _httpClient.PutAsync(endpoint, content);

            if (!TratarErrosResponse(response))
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Error updating relative {Relative}: {StatusCode}. Response: {ResponseContent}", relative, response.StatusCode, responseContent);
            }
            else
            {
                _logger.LogInformation("Successfully updated relative {Relative}", relative);
            }
        }

        public async Task RemoveRelative(Guid relativeId)
        {
            var endpoint = $"/api/relative/{relativeId}";
            var response = await _httpClient.DeleteAsync(endpoint);

            if (!TratarErrosResponse(response))
            {
                _logger.LogError("Error removing relative {RelativeId}: {StatusCode}", relativeId, response.StatusCode);
            }
        }
    }
}

using Familia.WebApp.MVC.Extensions;
using FML.WebApp.MVC.Extensions;
using FML.WebApp.MVC.Services.Interface;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace FML.WebApp.MVC.Services
{
    public class FamiliaService : Service, IFamiliaService
    {
        private readonly HttpClient _httpClient;
        private readonly IAspNetUser _user;

        public FamiliaService(HttpClient httpClient,
                                   IOptions<AppSettings> settings,
                                   ILogger<FamiliaService> logger,
                                   IAspNetUser user)
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            _user = user;
            _httpClient = new HttpClient(handler);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _user.ObterUserToken());
            _httpClient.BaseAddress = new Uri(settings.Value.FamiliaUrl);

        }

        public async Task<List<Relative>> GetRelativeByFamilyId(Guid familyId)
        {
            var endpoint = $"/family/{familyId}";
            var response = await _httpClient.GetAsync(endpoint);

            if (!TratarErrosResponse(response))
            {
                return await DeserializarObjetoResponse<List<Relative>>(response);
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
                return new Relative();
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
        }

        public async Task UpdateRelative(Relative relative)
        {
            var endpoint = "/api/relative/";
            var content = ObterConteudo(relative);

            var response = await _httpClient.PutAsync(endpoint, content);
        }

        public async Task RemoveRelative(Guid relativeId)
        {
            var endpoint = $"/api/relative/{relativeId}";
            var response = await _httpClient.DeleteAsync(endpoint);
        }
    }
}

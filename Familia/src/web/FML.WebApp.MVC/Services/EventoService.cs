using FML.Core.Data;
using FML.WebApp.MVC.Extensions;
using FML.WebApp.MVC.Models;
using FML.WebApp.MVC.Services.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FML.WebApp.MVC.Services
{
    public class EventoService : Service, IEventoService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<EventoService> _logger;
        private readonly IAspNetUser _user;

        public EventoService(HttpClient httpClient,
                         IOptions<AppSettings> settings,
                         ILogger<EventoService> logger,
                         IAspNetUser user)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.EventoUrl);
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

        public async Task<ListaDeDesejos> AddListaDeDesejo(ListaDeDesejos listaDeDesejos)
        {
            listaDeDesejos.Id = Guid.NewGuid();

            var content = ObterConteudo(listaDeDesejos);

            var response = await _httpClient.PostAsync("/api/lista-de-desejos", content);

            try
            {
                if (!TratarErrosResponse(response))
                {
                    return await DeserializarObjetoResponse<ListaDeDesejos>(response);
                }
            }
            catch
            {
                return new ListaDeDesejos();
            }

            return await DeserializarObjetoResponse<ListaDeDesejos>(response);
        }

        public async Task<List<ListaDeDesejos>> GetListaDeDesejos()
        {
            var response = await _httpClient.GetAsync("api/lista-de-desejos");

            var teste = await response.Content.ReadAsStringAsync();

            try
            {
                if (!TratarErrosResponse(response))
                {
                    return await DeserializarObjetoResponse<List<ListaDeDesejos>>(response);
                }
            }
            catch
            {
                return new List<ListaDeDesejos>();
            }

            return await DeserializarObjetoResponse<List<ListaDeDesejos>>(response);
        }

        public async Task<bool> EditListaDeDesejos(ListaDeDesejos listaDeDesejos)
        {
            var content = ObterConteudo(listaDeDesejos);
            var response = await _httpClient.PatchAsync($"api/lista-de-desejos/", content);
            return await DeserializarObjetoResponse<bool>(response);
        }

        public async Task DeleteListaDesejos(Guid id)
        {
            await _httpClient.DeleteAsync($"api/lista-de-desejos/{id.ToString()}");

        }
    }
}

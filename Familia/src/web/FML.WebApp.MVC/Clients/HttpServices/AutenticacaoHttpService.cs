using Familia.WebApp.MVC.Extensions;
using Familia.WebApp.MVC.Models;
using FML.WebApp.MVC.Clients.HttpServices.Interface;
using Microsoft.Extensions.Options;

namespace FML.WebApp.MVC.Clients.HttpServices
{
    public class AutenticacaoHttpService : HttpService, IAutenticacaoHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<AutenticacaoHttpService> _logger;

        public AutenticacaoHttpService(HttpClient httpClient,
                                   IOptions<AppSettings> settings,
                                   ILogger<AutenticacaoHttpService> logger)
        {
            if (settings?.Value?.AutenticacaoUrl == null)
            {
                throw new ArgumentNullException(nameof(settings.Value.AutenticacaoUrl), "AutenticacaoUrl cannot be null.");
            }

            _logger = logger;
            _logger.LogInformation("AutenticacaoUrl: {AutenticacaoUrl}", settings.Value.AutenticacaoUrl);

            // Bypass SSL certificate validation (for development purposes only)
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(settings.Value.AutenticacaoUrl)
            };
        }

        public async Task<UsuarioRespostaLogin> Login(UsuarioLogin usuarioLogin)
        {
            var loginContent = ObterConteudo(usuarioLogin);

            var response = await _httpClient.PostAsync("/api/identidade/autenticar", loginContent);

            var teste = await response.Content.ReadAsStringAsync();

            try
            {
                if (!TratarErrosResponse(response))
                {
                    return new UsuarioRespostaLogin
                    {
                        ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
                    };
                }
            }
            catch
            {
                return new UsuarioRespostaLogin
                {
                    ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
                };
            }


            return await DeserializarObjetoResponse<UsuarioRespostaLogin>(response);
        }

        public async Task<UsuarioRespostaLogin> Registro(UsuarioRegistro usuarioRegistro)
        {
            var registroContent = ObterConteudo(usuarioRegistro);

            try
            {
                var response = await _httpClient.PostAsync("/api/identidade/nova-conta", registroContent);

                if (!TratarErrosResponse(response))
                {
                    return new UsuarioRespostaLogin
                    {
                        ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
                    };
                }

                return await DeserializarObjetoResponse<UsuarioRespostaLogin>(response);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error connecting to the authentication service.");
                throw;
            }
        }
    }
}

using Familia.WebApp.MVC.Extensions;
using FML.WebApp.MVC.Services;
using FML.WebApp.MVC.Services.Handlers;
using FML.WebApp.MVC.Services.Interface;

namespace Familia.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddTransient<IAspNetUser, AspNetUser>();

            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<IAutenticacaoService, AutenticacaoService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<IEventoService, EventoService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<IFamiliaService, FamiliaService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddScoped<IEventoService, EventoService>();
            services.AddScoped<IFamiliaService, FamiliaService>();
        }
    }
}

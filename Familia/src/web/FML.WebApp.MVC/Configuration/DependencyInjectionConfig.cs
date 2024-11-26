using Familia.WebApp.MVC.Extensions;
using FML.WebApp.MVC.Clients.Handlers;
using FML.WebApp.MVC.Clients.HttpServices;
using FML.WebApp.MVC.Clients.HttpServices.Interface;
using FML.WebApp.MVC.Services;
using FML.WebApp.MVC.Services.Interfaces;

namespace Familia.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<IAutenticacaoHttpService, AutenticacaoHttpService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<IEventoHttpService, EventoHttpService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<IFamiliaHttpService, FamiliaHttpService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            services.AddScoped<IFamiliaService, FamiliaService>();
        }
    }
}

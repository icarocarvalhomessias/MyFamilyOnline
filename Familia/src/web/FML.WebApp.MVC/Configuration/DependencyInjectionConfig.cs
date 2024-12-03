using FML.Core.Data;
using FML.WebApp.MVC.Services;
using FML.WebApp.MVC.Services.Handlers;
using FML.WebApp.MVC.Services.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Familia.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Add services to the container.
            services.AddControllersWithViews();
            services.AddControllersWithViews();

            services.AddHttpContextAccessor();
            services.AddScoped<IAspNetUser, AspNetUser>();

            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
            services.AddHttpClient<IAutenticacaoService, AutenticacaoService>();
            services.AddHttpClient<IEventoService, EventoService>();
            services.AddHttpClient<IFamiliaService, FamiliaService>();

        }
    }
}

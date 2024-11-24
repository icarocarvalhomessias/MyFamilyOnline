using Familia.WebApp.MVC.Extensions;
using Familia.WebApp.MVC.Services;
using FML.WebApp.MVC.Services;
using FML.WebApp.MVC.Services.Interface;

namespace Familia.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddHttpClient<IAutenticacaoService, AutenticacaoService>();
            services.AddHttpClient<IEventoService, EventoService>();
            services.AddHttpClient<IFamiliaService, FamiliaService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();
        }
    }
}

using Familia.WebApp.MVC.Extensions;
using FML.WebApp.MVC.Clients.HttpServices;
using FML.WebApp.MVC.Clients.HttpServices.Interface;

namespace Familia.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddHttpClient<IAutenticacaoHttpService, AutenticacaoHttpService>();
            services.AddHttpClient<IEventoHttpService, EventoHttpService>();
            services.AddHttpClient<IFamiliaHttpService, FamiliaHttpService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();
        }
    }
}

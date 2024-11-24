using FML.Familiares.API.Data;
using FML.Familiares.API.Services;
using FML.Familiares.API.Services.Interface;

namespace FML.Familiares.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IFamilyService, FamilyService>();
            services.AddScoped<IRelativeService, RelativeService>();

            services.AddScoped<FamiliaresContext>();
        }
    }
}

using System.Text.Json.Serialization;

namespace FML.Familiares.API.Configuration
{
    public static class JsonConfig
    {
        public static void RegisterJson(this IServiceCollection services)
        {
            services.AddControllers()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                        options.JsonSerializerOptions.MaxDepth = 64; // Increase the maximum depth if needed
                    });
        }
    }
}

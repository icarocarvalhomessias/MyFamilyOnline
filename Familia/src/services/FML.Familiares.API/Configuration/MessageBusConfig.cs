using FML.Core.Utils;
using FML.Familiares.API.BackgroundServices;
using FML.MessageBus;

namespace FML.Familiares.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                .AddHostedService<FamiliarIntegrationHandler>();
        }
    }
}

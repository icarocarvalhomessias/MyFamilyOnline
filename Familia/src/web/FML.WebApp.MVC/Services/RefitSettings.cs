using Polly;
using Polly.Extensions.Http;
using Refit;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

public static class RefitConfig
{
    public static RefitSettings GetRefitSettings()
    {
        var jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            ReferenceHandler = ReferenceHandler.Preserve,
            Converters =
            {
                new JsonStringEnumConverter()
            },
            MaxDepth = 64 // Increase the maximum depth if needed
        };

        return new RefitSettings
        {
            ContentSerializer = new SystemTextJsonContentSerializer(jsonSerializerOptions)
        };
    }

    public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(new[]
            {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10),
            }, (outcome, timespan, retryCount, context) =>
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Tentando pela {retryCount} vez!");
                Console.ForegroundColor = ConsoleColor.White;
            });
    }

}

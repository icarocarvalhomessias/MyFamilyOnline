using Refit;
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
            MaxDepth = 64 // Increase the maximum depth if needed
        };

        return new RefitSettings
        {
            ContentSerializer = new SystemTextJsonContentSerializer(jsonSerializerOptions)
        };
    }
}

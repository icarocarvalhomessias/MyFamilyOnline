using Familia.Identidade.API.Data;
using FML.Identidade.API.Configuration;
using FML.WebApi.Core.Identidade;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration
           .SetBasePath(builder.Environment.ContentRootPath)
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
           .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
           .AddEnvironmentVariables();

        ConfigureServices(builder);

        var app = builder.Build();
        ConfigureMiddleware(app);
        app.Run();
    }

    private static void ConfigureServices(WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;

        builder.Services.AddIdentityConfiguration(configuration);
        builder.Services.AddApiConfiguration();
        builder.Services.AddSwaggerConfiguration();

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                options.JsonSerializerOptions.MaxDepth = 64;
            });
    }

    private static void ConfigureMiddleware(WebApplication app)
    {
        app.UseSwaggerConfiguration();

        app.UseApiConfiguration(app.Environment);

        app.UseCors("AllowAll");
    }

}

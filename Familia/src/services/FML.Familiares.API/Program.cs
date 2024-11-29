using FML.Familiares.API.Configuration;
using FML.WebApi.Core.Identidade;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;
        var hostEnvironment = builder.Environment;

        builder.Configuration
            .SetBasePath(hostEnvironment.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

        ConfigureServices(builder);
        var app = builder.Build();
        Configure(app);
        app.Run();
    }

    private static void ConfigureServices(WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        builder.Services.AddApiConfiguration(configuration);
        builder.Services.AddJwtConfiguration(configuration);
        builder.Services.AddSwaggerConfiguration();
        builder.Services.RegisterServices();
        builder.Services.RegisterJson();
    }

    private static void Configure(WebApplication app)
    {
        app.UseSwaggerConfiguration();

        app.UseApiConfiguration(app.Environment);
    }

}


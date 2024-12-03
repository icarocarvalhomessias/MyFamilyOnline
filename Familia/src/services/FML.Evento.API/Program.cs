using FML.Evento.API.Configuration;
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

        builder.Services.AddApiConfiguration(configuration);

        var app = builder.Build();
        app.UseApiConfiguration(app.Environment);
        app.Run();
    }
}


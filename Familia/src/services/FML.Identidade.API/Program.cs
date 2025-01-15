using FML.Identidade.API.Configuration;
using FML.MessageBus;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var configuration = builder.Configuration;
        configuration
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

        builder.Services.AddApiConfiguration(configuration);
        builder.Services.AddMessageBusConfiguration(configuration);

        var app = builder.Build();

        app.UseApiConfiguration(app.Environment);

        app.Run();
    }
}
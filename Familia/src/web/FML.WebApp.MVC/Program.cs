using Familia.WebApp.MVC.Configuration;
using Familia.WebApp.MVC.Extensions;
using FML.WebApp.MVC.Extensions;
using FML.WebApp.MVC.Services;
using FML.WebApp.MVC.Services.Handlers;
using FML.WebApp.MVC.Services.Interface;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configuração do host environment
        var hostEnvironment = builder.Environment;
        builder.Configuration
            .SetBasePath(hostEnvironment.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables();

        var appSettingsSection = builder.Configuration.GetSection("AppSettings");
        builder.Services.Configure<AppSettings>(appSettingsSection);

        ConfigureServices(builder);
        var app = builder.Build();
        
        Configure(app);
        app.Run();
    }
    private static void  ConfigureServices(WebApplicationBuilder builder)
    {
        

        builder.Services.AddIdentityConfiguration();
        builder.Services.AddControllersWithViews();
        builder.Services.AddMvcConfiguration();

        

        builder.Services.RegisterServices();
    }

    private static void Configure(WebApplication app)
    {

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseMiddleware<ExceptionMiddleware>();
        app.UseIdentityConfiguration();
        app.UseMvcConfiguration(app.Environment);

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Familia}/{id?}");
    }
}
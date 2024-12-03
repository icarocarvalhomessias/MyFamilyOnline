using Familia.WebApp.MVC.Configuration;
using Microsoft.AspNetCore.Builder;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.UseIdentityConfiguration();
        
        var app = builder.Build();
        app.UseIdentityConfiguration();

        app.Run();
    }
}

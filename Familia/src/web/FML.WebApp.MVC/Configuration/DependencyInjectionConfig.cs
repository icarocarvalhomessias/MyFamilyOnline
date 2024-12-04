using FML.Core.Data;
using FML.Familiares.API.Configuration;
using FML.WebApp.MVC.Services;
using FML.WebApp.MVC.Services.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;

namespace Familia.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
            services.AddScoped<IAspNetUser, AspNetUser>();

            var refitSettings = RefitConfig.GetRefitSettings();

            services.AddHttpClient("EventoRefit", options =>
            {
                var eventoUrl = configuration.GetSection("EventoUrl").Value;
                options.BaseAddress = new Uri(eventoUrl);
            })
                .AddHttpMessageHandler<AuthorizationHandler>()
                .ConfigurePrimaryHttpMessageHandler(() => new CustomHttpClientHandler())
                .AddTypedClient(client => RestService.For<IEventoServiceRefit>(client, refitSettings))
                .AddJsonOptions();

            services.AddHttpClient("FamiliaRefit", options =>
            {
                var familiaUrl = configuration.GetSection("FamiliaUrl").Value;
                options.BaseAddress = new Uri(familiaUrl);
            })
                .AddHttpMessageHandler<AuthorizationHandler>()
                .ConfigurePrimaryHttpMessageHandler(() => new CustomHttpClientHandler())
                .AddTypedClient(client => RestService.For<IFamiliaServiceRefit>(client, refitSettings))
                .AddJsonOptions();

            services.AddHttpClient("AuteRefit", options =>
            {
                var familiaUrl = configuration.GetSection("FamiliaUrl").Value;
                options.BaseAddress = new Uri(familiaUrl);
            })
                .AddHttpMessageHandler<AuthorizationHandler>()
                .ConfigurePrimaryHttpMessageHandler(() => new CustomHttpClientHandler())
                .AddTypedClient(client => RestService.For<IFamiliaServiceRefit>(client, refitSettings))
                .AddJsonOptions();

            services.AddTransient<AuthorizationHandler>();
            services.AddHttpClient<IAutenticacaoService, AutenticacaoService>(options =>
            {
                var autenticacaoUrl = configuration.GetSection("AutenticacaoUrl").Value;
                options.BaseAddress = new Uri(autenticacaoUrl);
            })
            .ConfigurePrimaryHttpMessageHandler(() => new CustomHttpClientHandler())
            .AddJsonOptions();
        }
    }
}

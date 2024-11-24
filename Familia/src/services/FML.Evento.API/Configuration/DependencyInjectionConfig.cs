﻿using FML.Evento.API.Services;
using FML.Evento.API.Services.Interface;

namespace FML.Evento.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IEventoService, EventoService>();
        }
    }
}
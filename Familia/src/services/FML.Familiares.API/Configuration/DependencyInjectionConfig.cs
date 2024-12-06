using FML.Familiares.API.Data;
using FML.Familiares.API.Data.Repository.Interface;
using FML.Familiares.API.Data.Repository;
using FML.Familiares.API.Services;
using FML.Familiares.API.Services.Interface;
using FML.Core.Mediator;
using MediatR;
using FML.Familiares.API.Application.Commands;
using FluentValidation.Results;
using FML.Familiares.API.Application.Events;

namespace FML.Familiares.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IRequestHandler<RegistrarFamiliarCommand, ValidationResult>, FamiliarCommandHandler>();
            services.AddScoped<INotificationHandler<FamiliarRegistradoEvent>, FamiliarEventHandler>();

            services.AddScoped<IRelativeRepository, RelativeRepository>();
            services.AddScoped<IHouseRepository, HouseRepository>();
            services.AddScoped<IFamilyRepository, FamilyRepository>();


            services.AddScoped<IFamilyService, FamilyService>();
            services.AddScoped<IRelativeService, RelativeService>();
            services.AddScoped<IHouseService, HouseService>();

            services.AddScoped<FamiliaresContext>();
        }
    }
}

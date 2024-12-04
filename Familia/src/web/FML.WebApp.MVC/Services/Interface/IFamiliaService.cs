using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FML.WebApp.MVC.Services.Interface;

public interface IFamiliaServiceRefit
{

    [Get("/api/familias")]
    Task<List<Family>> GetFamilies();

    [Get("/api/casas/")]
    Task<List<House>> GetHousesByFamilyId(Guid familyId);

    [Get("/api/familiares")]
    Task<List<Relative>> GetRelatives();

    [Get("/api/familiares/{relativeId}")]
    Task<Relative> GetRelativeById(Guid relativeId);

    [Post("/api/familiares")]
    Task AddRelative(Relative relative);

    [Put("/api/familiares")]
    Task UpdateRelative(Relative relative);

    [Delete("/api/familiares/{relativeId}")]
    Task RemoveRelative(Guid relativeId);

}

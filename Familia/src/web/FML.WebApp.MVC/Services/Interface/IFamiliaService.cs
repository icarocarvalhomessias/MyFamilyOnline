using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FML.WebApp.MVC.Services.Interface;

public interface IFamiliaService
{
    Task<List<Relative>> GetRelativeByFamilyId(Guid familyId);

    Task<Relative> GetRelativeById(Guid relativeId);

    Task<List<Family>> GetFamilies();

    Task<List<House>> GetHousesByFamilyId(Guid familyId);

    Task AddRelative(Relative relative);
    Task UpdateRelative(Relative relative);
    Task RemoveRelative(Guid relativeId);

}

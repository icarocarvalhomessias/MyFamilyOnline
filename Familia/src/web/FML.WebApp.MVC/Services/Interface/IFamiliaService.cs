using Familia.WebApp.MVC.Models;

public interface IFamiliaService
{
    Task<List<Relative>> GetRelativeByFamilyId(Guid familyId);
}

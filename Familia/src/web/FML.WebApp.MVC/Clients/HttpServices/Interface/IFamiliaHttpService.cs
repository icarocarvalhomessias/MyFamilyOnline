namespace FML.WebApp.MVC.Clients.HttpServices.Interface;

public interface IFamiliaHttpService
{
    Task<List<Relative>> GetRelativeByFamilyId(Guid familyId);

    Task<Relative> GetRelativeById(Guid relativeId);

    Task<List<Family>> GetFamilies();

    Task<List<House>> GetHousesByFamilyId(Guid familyId);

    Task AddRelative(Relative relative);
    Task UpdateRelative(Relative relative);
    Task RemoveRelative(Guid relativeId);

}

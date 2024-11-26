namespace FML.WebApp.MVC.Services.Interfaces
{
    public interface IFamiliaService
    {
        Task<Dictionary<string, Dictionary<Relative, Relative>>> GetFamilyTree(Guid familyId);
        Task AddRelative(Relative relative);

        Task UpdateRelative(Relative relative);

        Task RemoveRelative(Guid relativeId);

        Task<Relative> GetRelativeById(Guid relativeId);
        Task<List<Relative>> GetRelativesByFamilyId(Guid familyId);

        Task<List<Family>> GetFamilies();

        Task<List<House>> GetHousesByFamilyId(Guid familyId);


    }
}

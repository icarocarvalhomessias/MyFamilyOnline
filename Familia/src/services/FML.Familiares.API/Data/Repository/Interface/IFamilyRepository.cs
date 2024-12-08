using FML.Core.Data;

public interface IFamilyRepository : IRepository<Family>
{
    Task<IEnumerable<Family>> GetFamiliesByFamilyId(Guid familyId);
    Task<IEnumerable<Family>> GetAll();


    Task<bool> AddFamily(Family family);
    Task<bool> UpdateFamily(Family family);
    Task<bool> RemoveFamily(Family family);
    Task<bool> AddRelative(Relative relative);
}

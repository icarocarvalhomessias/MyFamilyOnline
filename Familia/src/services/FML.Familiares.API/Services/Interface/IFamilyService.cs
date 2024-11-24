public interface IFamilyService
{
    Task<bool> AddFamily(Family family);
    Task<bool> UpdateFamily(Guid id, Family family);
    Task<bool> DeleteFamily(Guid id);
    Task<Family> GetFamilyById(Guid id);
}

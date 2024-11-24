using FML.Core.Data;

namespace FML.Familiares.API.Data.Repository.Interface
{
    public interface IFamilyRepository : IRepository<Family>
    {
        Task<IEnumerable<Family>> GetFamiliesByFamilyId(Guid familyId);
        Task<IEnumerable<Family>> GetFamiliesByHouseId(Guid houseId);
        Task<bool> AddFamily(Family family);
        Task<bool> UpdateFamily(Family family);
        Task<bool> RemoveFamily(Family family);
    }
}

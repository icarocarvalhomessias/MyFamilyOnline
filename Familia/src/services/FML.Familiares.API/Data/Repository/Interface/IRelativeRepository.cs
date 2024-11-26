using FML.Core.Data;

namespace FML.Familiares.API.Data.Repository.Interface
{
    public interface IRelativeRepository : IRepository<Relative>
    {

        Task<IEnumerable<Relative>> GetRelativesByFamilyId(Guid familyId);
        Task<IEnumerable<Relative>> GetRelativesByHouseId(Guid houseId);
        Task<IEnumerable<Relative>> GetRelativesByFatherId(Guid fatherId);
        Task<IEnumerable<Relative>> GetRelativesByMotherId(Guid motherId);
        Task AddRelative(Relative relative);
        Task AddRelatives(IEnumerable<Relative> relatives);
        Task<bool> UpdateRelative(Relative relative);
        Task<bool> RemoveRelative(Guid relativeId);

        Task<Relative> GetRelativeById(Guid relativeId);

    }
}

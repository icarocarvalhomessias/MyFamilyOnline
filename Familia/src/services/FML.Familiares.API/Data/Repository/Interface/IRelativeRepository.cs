using FML.Core.Data;

namespace FML.Familiares.API.Data.Repository.Interface
{
    public interface IRelativeRepository : IRepository<Familiar>
    {

        Task<IEnumerable<Familiar>> GetRelativesByFamilyId(Guid familyId);
        Task<IEnumerable<Familiar>> GetRelativesByHouseId(Guid houseId);
        Task<IEnumerable<Familiar>> GetRelativesByFatherId(Guid fatherId);
        Task<IEnumerable<Familiar>> GetRelativesByMotherId(Guid motherId);
        void AddRelative(Familiar relative);
        Task AddRelatives(IEnumerable<Familiar> relatives);
        Task<bool> UpdateRelative(Familiar relative);
        Task<bool> RemoveRelative(Guid relativeId);

        Task<Familiar?> GetRelativeById(Guid relativeId);

    }
}

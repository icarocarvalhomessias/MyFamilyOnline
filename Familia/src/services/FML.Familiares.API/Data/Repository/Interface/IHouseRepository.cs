using FML.Core.Data;

namespace FML.Familiares.API.Data.Repository.Interface
{
    public interface IHouseRepository : IRepository<House>
    {
        Task<IEnumerable<House>> GetHousesByFamilyId(Guid familyId);
        Task AddHouse(House house);
    }
}

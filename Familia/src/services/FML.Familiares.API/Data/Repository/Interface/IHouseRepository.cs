using FML.Core.Data;

namespace FML.Familiares.API.Data.Repository.Interface
{
    public interface IHouseRepository : IRepository<House>
    {
        Task<IEnumerable<House>> GetHousesByFamilyId(Guid familyId);
        Task<IEnumerable<House>> GetHousesByHouseId(Guid houseId);
        Task AddHouse(House house);
        void UpdateHouse(House house);
        void RemoveHouse(House house);
    }
}

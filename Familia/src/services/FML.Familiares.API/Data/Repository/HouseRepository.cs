using FML.Core.Data;
using FML.Familiares.API.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace FML.Familiares.API.Data.Repository
{
    public class HouseRepository : IHouseRepository
    {
        private readonly FamiliaresContext _context;


        public HouseRepository(FamiliaresContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<House>> GetHousesByFamilyId(Guid familyId)
        {
            return await _context.Houses.AsNoTracking().Where(h => h.FamilyId == familyId).ToListAsync();
        }

        public async Task<IEnumerable<House>> GetHousesByHouseId(Guid houseId)
        {
            return await _context.Houses.AsNoTracking().Where(h => h.Id == houseId).ToListAsync();
        }


        public async Task AddHouse(House house)
        {
            await _context.Houses.AddAsync(house);
            await _context.SaveChangesAsync(); // Certifique-se de salvar as mudanças
        }

        public void RemoveHouse(House house)
        {
            house.IsActive = false;
            UpdateHouse(house);
        }

        public void UpdateHouse(House house)
        {
            _context.Update(house);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}

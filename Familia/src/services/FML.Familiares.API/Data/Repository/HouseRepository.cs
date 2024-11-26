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

        public async Task AddHouse(House house)
        {
            await _context.Houses.AddAsync(house);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}

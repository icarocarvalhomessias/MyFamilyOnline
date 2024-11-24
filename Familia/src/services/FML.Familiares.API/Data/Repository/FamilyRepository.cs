using FML.Core.Data;
using FML.Familiares.API.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace FML.Familiares.API.Data.Repository
{
    public class FamilyRepository : IFamilyRepository
    {
        private readonly FamiliaresContext _context;

        public FamilyRepository(FamiliaresContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Family>> GetFamiliesByFamilyId(Guid familyId)
        {
            return await _context.Families.AsNoTracking().Where(f => f.Id == familyId).ToListAsync();
        }

        public async Task<IEnumerable<Family>> GetFamiliesByHouseId(Guid houseId)
        {
            return await _context.Families
                .AsNoTracking()
                .Where(f => f.Houses.Any(h => h.Id == houseId))
                .ToListAsync();
        }

        public async Task<bool> AddFamily(Family family)
        {
            _context.Add(family);
            return await _context.Commit();
        }

        public async Task<bool> RemoveFamily(Family family)
        {
            family.IsActive = false;
            _context.Update(family);
            return await _context.Commit();
        }

        public async Task<bool> UpdateFamily(Family family)
        {
            _context.Update(family);
            return await _context.Commit();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}

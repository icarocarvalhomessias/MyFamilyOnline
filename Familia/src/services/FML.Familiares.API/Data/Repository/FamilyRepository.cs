using FML.Core.Data;
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


        public async Task<bool> AddFamily(Family family)
        {
            _context.Families.Add(family);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateFamily(Family family)
        {
            _context.Families.Update(family);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveFamily(Family family)
        {
            _context.Families.Remove(family);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Family>> GetFamiliesByFamilyId(Guid familyId)
        {
            return await _context.Families
                .Include(f => f.Relatives)
                .Include(f => f.Houses)
                .Where(f => f.Id == familyId)
                .ToListAsync();
        }

        public async Task<bool> AddRelative(Relative relative)
        {
            _context.Relatives.Add(relative);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Family>> GetAll()
        {
            return await _context.Families
                .AsNoTracking()
                .ToListAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}

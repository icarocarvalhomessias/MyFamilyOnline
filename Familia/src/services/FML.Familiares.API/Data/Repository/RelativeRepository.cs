using FML.Core.Data;
using FML.Familiares.API.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace FML.Familiares.API.Data.Repository
{
    public class RelativeRepository : IRelativeRepository
    {
        private readonly FamiliaresContext _context;

        public RelativeRepository(FamiliaresContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Familiar>> GetRelativesByFamilyId(Guid familyId)
        {
            return await _context.Relatives
                .Include(r => r.Family)
                .Include(r => r.House)
                .AsNoTracking()
                .Where(r => r.FamilyId == familyId).ToListAsync();
        }

        public async Task<IEnumerable<Familiar>> GetRelativesByFatherId(Guid fatherId)
        {
            return await _context.Relatives.AsNoTracking().Where(r => r.FatherId == fatherId).ToListAsync();
        }

        public async Task<IEnumerable<Familiar>> GetRelativesByHouseId(Guid houseId)
        {
            return await _context.Relatives.AsNoTracking().Where(r => r.HouseId == houseId).ToListAsync();
        }

        public async Task<IEnumerable<Familiar>> GetRelativesByMotherId(Guid motherId)
        {
            return await _context.Relatives.AsNoTracking().Where(r => r.MotherId == motherId).ToListAsync();
        }

        public void AddRelative(Familiar relative)
        {
            _context.Relatives.Add(relative);
        }

        public async Task AddRelatives(IEnumerable<Familiar> relatives)
        {
            if (relatives == null || !relatives.Any())
            {
                throw new ArgumentException("The relatives collection is null or empty.", nameof(relatives));
            }

            await _context.Relatives
                .AddRangeAsync(relatives);
        }


        public async Task<bool> RemoveRelative(Guid relativeId)
        {
            _context.Relatives
                .Remove(new Familiar { Id = relativeId });
            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<bool> UpdateRelative(Familiar relative)
        {
            _context.Relatives
                .Update(relative);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Familiar?> GetRelativeById(Guid relativeId)
        {
            var teste123 = _context.Relatives.Where(x => x.Id == relativeId).ToList();

            return teste123.Any() ? teste123.First() : null;
        }



        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}

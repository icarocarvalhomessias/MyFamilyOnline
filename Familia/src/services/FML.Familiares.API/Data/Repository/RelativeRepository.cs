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

        public async Task<IEnumerable<Relative>> GetRelativesByFamilyId(Guid familyId)
        {
            return await _context.Relatives
                .Include(r => r.Family)
                .Include(r => r.House)
                .AsNoTracking()
                .Where(r => r.FamilyId == familyId).ToListAsync();
        }

        public async Task<IEnumerable<Relative>> GetRelativesByFatherId(Guid fatherId)
        {
            return await _context.Relatives.AsNoTracking().Where(r => r.FatherId == fatherId).ToListAsync();
        }

        public async Task<IEnumerable<Relative>> GetRelativesByHouseId(Guid houseId)
        {
            return await _context.Relatives.AsNoTracking().Where(r => r.HouseId == houseId).ToListAsync();
        }

        public async Task<IEnumerable<Relative>> GetRelativesByMotherId(Guid motherId)
        {
            return await _context.Relatives.AsNoTracking().Where(r => r.MotherId == motherId).ToListAsync();
        }

        public async Task AddRelative(Relative relative)
        {
            await _context.Relatives.AddAsync(relative);
            await _context.SaveChangesAsync(); // Certifique-se de salvar as mudanças

        }

        public async Task AddRelatives(IEnumerable<Relative> relatives)
        {
            try
            {
                if (relatives == null || !relatives.Any())
                {
                    throw new ArgumentException("The relatives collection is null or empty.", nameof(relatives));
                }

                await _context.Relatives.AddRangeAsync(relatives);
                await _context.SaveChangesAsync(); // Certifique-se de salvar as mudanças
            }
            catch (Exception ex)
            {
                // Log a exceção (use seu mecanismo de logging preferido)
                Console.WriteLine($"An error occurred while adding relatives: {ex.Message}");
                throw; // Re-throw a exceção para que ela possa ser tratada em outro lugar, se necessário
            }
        }


        public async Task<bool> RemoveRelative(Guid relativeId)
        {
            _context.Relatives.Remove(new Relative { Id = relativeId });
            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<bool> UpdateRelative(Relative relative)
        {
            _context.Relatives.Update(relative);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Relative> GetRelativeById(Guid relativeId)
        {
            return await _context.Relatives.AsNoTracking().FirstOrDefaultAsync(r => r.Id == relativeId);
        }


        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}

namespace FML.Familiares.API.Services
{
    public class FamilyService : IFamilyService
    {
        private readonly IFamilyRepository _familyRepository;

        public FamilyService(IFamilyRepository familyRepository)
        {
            _familyRepository = familyRepository;
        }

        public async Task<bool> AddFamily(Family family)
        {
            return await _familyRepository.AddFamily(family);
        }

        public async Task<bool> UpdateFamily(Guid id, Family family)
        {
            var existingFamily = await _familyRepository.GetFamiliesByFamilyId(id);
            if (existingFamily == null) return false;

            return await _familyRepository.UpdateFamily(family);
        }

        public async Task<bool> DeleteFamily(Guid id)
        {
            var family = await _familyRepository.GetFamiliesByFamilyId(id);
            if (family == null) return false;

            return await _familyRepository.RemoveFamily(family.First());
        }

        public async Task<Family> GetFamilyById(Guid id)
        {
            var family = await _familyRepository.GetFamiliesByFamilyId(id);
            return family.FirstOrDefault();
        }

        public async Task<List<Family>> GetFamilies()
        {
            var families = await _familyRepository.GetAll();
            return families.ToList();
        }
    }
}

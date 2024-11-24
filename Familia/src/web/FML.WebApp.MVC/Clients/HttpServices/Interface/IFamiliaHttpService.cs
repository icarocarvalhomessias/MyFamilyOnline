public interface IFamiliaHttpService
{
    Task<List<Relative>> GetRelativeByFamilyId(Guid familyId);
    //Task<Dictionary<string, Dictionary<Relative, Relative>>> GetRelativeByFamilyIdAsync(Guid familyId);
}

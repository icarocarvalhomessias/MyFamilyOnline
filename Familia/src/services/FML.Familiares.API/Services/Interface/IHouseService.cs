namespace FML.Familiares.API.Services.Interface
{
    public interface IHouseService
    {
        Task<IEnumerable<House>> GetHousesByFamilyId(Guid familyId);
    }
}

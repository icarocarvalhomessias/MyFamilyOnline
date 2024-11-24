namespace FML.Familiares.API.Services.Interface
{
    public interface IRelativeService
    {
        Task<IEnumerable<Relative>> GetRelativeByFamilyId(Guid id);
        Task<Guid> Add();
    }
}

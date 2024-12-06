namespace FML.Familiares.API.Services.Interface
{
    public interface IRelativeService
    {
        Task<IEnumerable<Familiar>> GetRelatives();
        Task<Guid> Add();
        Task<bool> Update(Familiar relative);
        Task<bool> AddRelative(Familiar relative);
        Task<bool> RemoveRelative(Guid relativeId);
        Task<Familiar> GetRelativeById(Guid relativeId);
    }
}

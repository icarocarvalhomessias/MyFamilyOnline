using FML.Familiares.API.Models;

namespace FML.Familiares.API.Services.Interface
{
    public interface IRelativeService
    {
        Task<IEnumerable<Relative>> GetRelatives();
        Task<Guid> CargaInicial();
        Task<bool> RemoveRelative(Guid relativeId);
        Task<Relative> GetRelativeById(Guid relativeId);
    }
}

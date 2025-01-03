namespace FML.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}

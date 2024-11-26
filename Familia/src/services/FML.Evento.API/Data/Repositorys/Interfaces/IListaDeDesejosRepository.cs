using FML.Core.Data;
using FML.Evento.API.Data.Entities;

namespace FML.Evento.API.Data.Repositorys.Interfaces
{
    public interface IListaDeDesejosRepository : IRepository<ListaDeDesejos>
    {
        Task<IEnumerable<ListaDeDesejos>> GetListaDeDesejos();
        Task<bool> AddAsync(ListaDeDesejos listaDeDesejos);
    }
}

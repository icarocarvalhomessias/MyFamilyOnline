using FML.Core.Data;
using FML.Evento.API.Data.Entities;
using FML.Evento.API.Data.Repositorys.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FML.Evento.API.Data.Repositorys
{
    public class ListaDeDesejosRepository : IListaDeDesejosRepository
    {
        private readonly EventoContext _context;
        public IUnitOfWork UnitOfWork => _context;


        public ListaDeDesejosRepository(EventoContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<ListaDeDesejos>> GetListaDeDesejos()
        {
            return await _context.ListasDeDesejos.AsNoTracking().ToListAsync();
        }

        public async Task<bool> AddAsync(ListaDeDesejos listaDeDesejos)
        {
            await _context.ListasDeDesejos.AddAsync(listaDeDesejos);

            return await _context.SaveChangesAsync() > 0;
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

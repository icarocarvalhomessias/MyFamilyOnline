using FML.Evento.API.Services.Interface;

namespace FML.Evento.API.Services
{
    using FML.Evento.API.Controllers;
    using FML.Evento.API.Data.Entities;
    using FML.Evento.API.Data.Repositorys.Interfaces;
    using FML.Evento.API.Models;

    public class EventoService : IEventoService
    {

        private readonly IListaDeDesejosRepository _listaDeDesejosRepository;

        public EventoService(IListaDeDesejosRepository listaDeDesejosRepository)
        {
            _listaDeDesejosRepository = listaDeDesejosRepository;
        }


        public async Task<IEnumerable<ListaDeDesejos>> ListaDeDesejos()
        {
            return await _listaDeDesejosRepository.GetListaDeDesejos();
        }

        public async Task<bool> AddListaDeDesejos(ListaDeDesejos desejo)
        {
            return await _listaDeDesejosRepository.AddAsync(desejo);
        }


        public List<SecretSantaPair> RealizarAmigoOculto(Guid familiaId)
        {
            return FamiliaCarvalhoStub.DrawSecretSanta();
        }

        public List<SecretSantaPair> RefazAmigoOculto(Guid familiaId)
        {
            return FamiliaCarvalhoStub.RetryDrawSecretSanta();
        }

        public Task<EventoModel> Adicionar(EventoModel evento)
        {
            // Implement the logic to add an event
            throw new NotImplementedException();
        }

        public Task<EventoModel> Atualizar(EventoModel evento)
        {
            // Implement the logic to update an event
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EventoModel>> ObterEventoPorData(DateTime data)
        {
            // Implement the logic to get events by date
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EventoModel>> ObterEventoPorTema(string tema)
        {
            // Implement the logic to get events by theme
            throw new NotImplementedException();
        }

        public Task<EventoModel> ObterPorId(Guid id)
        {
            // Implement the logic to get an event by id
            throw new NotImplementedException();
        }

        public List<Parente> ObterTodos()
        {
            return FamiliaCarvalhoStub.GetFamiliaCarvalho();
        }

        public Task<bool> Remover(Guid id)
        {
            // Implement the logic to remove an event
            throw new NotImplementedException();
        }

       
    }
}

﻿using FML.Evento.API.Services.Interface;

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

        public async Task<bool> UpdateListaDeDesejos(ListaDeDesejos desejo)
        {
            var listaDeDesejos = await _listaDeDesejosRepository.GetListaDeDesejos();

            var desejoToUpdate = listaDeDesejos.FirstOrDefault(d => d.Id == desejo.Id);

            if (desejoToUpdate == null)
                return false;

            desejoToUpdate.Nome = desejo.Nome;
            desejoToUpdate.Descricao = desejo.Descricao;
            desejoToUpdate.Preco = desejo.Preco;
            desejoToUpdate.Link = desejo.Link;
            desejoToUpdate.Loja = desejo.Loja;
            desejoToUpdate.Observacao = desejo.Observacao;
            desejoToUpdate.ParenteId = desejo.ParenteId;


            return await _listaDeDesejosRepository.UpdateAsync(desejoToUpdate);

        }

        public async Task<bool> DeleteListaDeDesejos(Guid id)
        {
            var listaDeDesejos = await _listaDeDesejosRepository.GetListaDeDesejos();

            var desejoToDelete = listaDeDesejos.FirstOrDefault(d => d.Id == id);

            if (desejoToDelete == null)
                return false;

            return await _listaDeDesejosRepository.Delete(desejoToDelete);
        }
    }
}

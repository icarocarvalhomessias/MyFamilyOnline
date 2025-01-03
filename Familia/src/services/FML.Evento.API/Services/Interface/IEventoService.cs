namespace FML.Evento.API.Services.Interface
{
    using FML.Evento.API.Controllers;
    using FML.Evento.API.Data.Entities;
    using FML.Evento.API.Models;

    public interface IEventoService
    {
        List<Parente> ObterTodos();
        Task<EventoModel> ObterPorId(Guid id);
        Task<IEnumerable<EventoModel>> ObterEventoPorTema(string tema);
        Task<IEnumerable<EventoModel>> ObterEventoPorData(DateTime data);
        Task<EventoModel> Adicionar(EventoModel evento);
        Task<EventoModel> Atualizar(EventoModel evento);
        Task<bool> Remover(Guid id);

        List<SecretSantaPair> RealizarAmigoOculto(Guid familiaId);
        List<SecretSantaPair> RefazAmigoOculto(Guid familiaId);

        Task<IEnumerable<ListaDeDesejos>> ListaDeDesejos();
        Task<bool> AddListaDeDesejos(ListaDeDesejos desejo);
        Task<bool> UpdateListaDeDesejos(ListaDeDesejos desejo);
        Task<bool> DeleteListaDeDesejos(Guid id);
    }
}

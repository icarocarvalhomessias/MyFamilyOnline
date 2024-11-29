using Familia.WebApp.MVC.Models;
using FML.WebApp.MVC.Models;

namespace FML.WebApp.MVC.Services.Interface
{
    public interface IEventoService
    {
        Task<List<SecretSantaPair>> RealizaAmigoOculto();
        Task<List<SecretSantaPair>> RefazAmigoOculto();

        Task<ListaDeDesejos> AddListaDeDesejo(ListaDeDesejos listaDeDesejos);
        Task<List<ListaDeDesejos>> GetListaDeDesejos();

        Task<bool> EditListaDeDesejos(ListaDeDesejos listaDeDesejos);

        Task DeleteListaDesejos(Guid id);


    }
}

using Familia.WebApp.MVC.Models;

namespace FML.WebApp.MVC.Clients.HttpServices.Interface
{
    public interface IEventoHttpService
    {
        Task<List<SecretSantaPair>> RealizaAmigoOculto();
        Task<List<SecretSantaPair>> RefazAmigoOculto();

        Task<List<Parente>> GetParentes();

    }
}

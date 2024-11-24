using Familia.WebApp.MVC.Models;

namespace FML.WebApp.MVC.Services.Interface
{
    public interface IEventoService
    {
        Task<List<SecretSantaPair>> RealizaAmigoOculto();
        Task<List<SecretSantaPair>> RefazAmigoOculto();

        Task<List<Parente>> GetParentes();

    }
}

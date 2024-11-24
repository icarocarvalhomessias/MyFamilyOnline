using Familia.WebApp.MVC.Models;

namespace FML.WebApp.MVC.Clients.HttpServices.Interface
{
    public interface IAutenticacaoHttpService
    {
        Task<UsuarioRespostaLogin> Login(UsuarioLogin usuarioLogin);

        Task<UsuarioRespostaLogin> Registro(UsuarioRegistro usuarioRegistro);
    }
}

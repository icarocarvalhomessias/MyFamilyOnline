using Familia.WebApp.MVC.Models;
using System.Threading.Tasks;

namespace FML.WebApp.MVC.Services.Interface
{
    public interface IAutenticacaoService
    {
        Task<UsuarioRespostaLogin> Login(UsuarioLogin usuarioLogin);

        Task<UsuarioRespostaLogin> Registro(UsuarioRegistro usuarioRegistro);
    }
}

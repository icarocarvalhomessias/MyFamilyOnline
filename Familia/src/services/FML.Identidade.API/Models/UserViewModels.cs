using System.ComponentModel.DataAnnotations;

namespace Familia.Identidade.API.Models
{
    public class UsuarioRegistro
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Senha { get; set; }

        [Compare("Senha", ErrorMessage = "As senhas não conferem.")]
        public string SenhaConfirmacao { get; set; }

        public UsuarioRegistro()
        {
            Email = string.Empty;
            Senha = string.Empty;
            SenhaConfirmacao = string.Empty;
        }
    }

    public class UsuarioLogin
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Senha { get; set; }

        public UsuarioLogin()
        {
            Email = string.Empty;
            Senha = string.Empty;
        }
    }

    public class UsuarioRespostaLogin
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UsuarioToken UsuarioToken { get; set; }

        public UsuarioRespostaLogin()
        {
            AccessToken = string.Empty;
            UsuarioToken = new UsuarioToken();
        }
    }

    public class UsuarioToken
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<UsuarioClaim> Claims { get; set; }

        public UsuarioToken()
        {
            Id = string.Empty;
            Email = string.Empty;
            Claims = new List<UsuarioClaim>();
        }
    }

    public class UsuarioClaim
    {
        public string Value { get; set; }
        public string Type { get; set; }

        public UsuarioClaim()
        {
            Value = string.Empty;
            Type = string.Empty;
        }
    }
}

namespace FML.Core.Data
{
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
}

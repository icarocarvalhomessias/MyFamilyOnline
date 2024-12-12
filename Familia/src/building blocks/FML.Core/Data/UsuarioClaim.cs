namespace FML.Core.Data
{
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

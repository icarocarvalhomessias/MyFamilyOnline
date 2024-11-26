namespace FML.Evento.API.Models
{
    public class DesejoModel
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string? Preco { get; set; }
        public string? Link { get; set; }
        public string? Loja { get; set; }
        public string? Observacao { get; set; }
        public Guid ParenteId { get; set; }
    }
}

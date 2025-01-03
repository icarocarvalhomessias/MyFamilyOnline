using FML.Core.DomainObjects;

namespace FML.Evento.API.Data.Entities
{
    public class ListaDeDesejos : IAggregateRoot
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string? Link { get; set; }
        public string? Preco { get; set; }
        public string? Loja { get; set; }
        public string? Observacao { get; set; }

        public Guid ParenteId { get; set; }
    }
}

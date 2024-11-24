namespace Familia.WebApp.MVC.Models
{
    public class Parente
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Casa { get; set; }
        public string Telefone { get; set; }
        public string LinkNome { get; set; }
        public string Genero { get; set; }
        public string Mae { get; set; }
        public string Pai { get; set; }
        public bool Patriarca { get; set; }
        public bool Matriarca { get; set; }
        public string Conjugue { get; set; }
        public List<Parente> Filhos { get; set; }
    }
}

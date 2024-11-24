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
    public bool Patriarca { get; set; } = false;
    public bool Matriarca { get; set; } = false;
    public string Conjugue { get; set; }

    public bool ParticipaAmigoOculto { get; set; }
    public List<Parente> Filhos { get; set; } = new List<Parente>();

}

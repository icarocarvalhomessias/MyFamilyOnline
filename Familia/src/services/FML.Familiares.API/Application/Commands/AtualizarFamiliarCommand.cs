using FluentValidation;
using FML.Core.Messages;

namespace FML.Familiares.API.Application.Commands
{
    public class AtualizarFamiliarCommand : Command
    {
        public Guid Id { get; private set; }

        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public Guid Familia { get; private set; }
        public Guid Casa { get; private set; }
        public Guid Pai { get; private set; }
        public Guid Mae { get; private set; }
        public string NomeLink { get; private set; }
        public string Foto { get; private set; }
        public bool AmigoSecreto { get; private set; }
        public string Email { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public bool Ativo { get; private set; }
        public bool EstaVivo { get; private set; }
        public bool Patriarca { get; private set; }
        public bool Matriarca { get; private set; }
        public DateTime DataFalecimento { get; private set; }
        public string Genero { get; private set; }
        public string? FotoFileBase64 { get; private set; }
        public string? FileName { get; private set; }
        public Guid? SpouseId { get; private set; }

        public AtualizarFamiliarCommand(Guid id, string nome, string sobrenome, Guid familia, Guid casa, Guid pai, Guid mae, string nomeLink, string foto, bool amigoSecreto, string email, DateTime dataNascimento, bool ativo, bool estaVivo, bool patriarca, bool matriarca, DateTime dataFalecimento, string genero, string? fotoFileBase64, string? fileName, Guid? spouseId)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            Familia = familia;
            Casa = casa;
            Pai = pai;
            Mae = mae;
            NomeLink = nomeLink;
            Foto = foto;
            AmigoSecreto = amigoSecreto;
            Email = email;
            DataNascimento = dataNascimento;
            Ativo = ativo;
            EstaVivo = estaVivo;
            Patriarca = patriarca;
            Matriarca = matriarca;
            DataFalecimento = dataFalecimento;
            Genero = genero;
            FotoFileBase64 = fotoFileBase64;
            FileName = fileName;
            SpouseId = spouseId;
        }
    }
}

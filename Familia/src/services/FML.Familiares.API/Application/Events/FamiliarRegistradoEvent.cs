using FML.Core.Messages;

namespace FML.Familiares.API.Application.Events
{
    public class FamiliarRegistradoEvent : Event
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public Gender Gender { get; private set; }

        public FamiliarRegistradoEvent(Guid id, string nome, string email, DateTime dataNascimento, Gender gender)
        {
            AggregateId = id;
            Id = id;
            Nome = nome;
            Email = email;
            BirthDate = dataNascimento;
            Gender = gender;
        }
    }
}

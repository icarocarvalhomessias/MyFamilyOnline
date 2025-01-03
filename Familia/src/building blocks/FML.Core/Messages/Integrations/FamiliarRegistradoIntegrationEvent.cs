using FML.Core.Data;

namespace FML.Core.Messages.Integrations
{
    public class FamiliarRegistradoIntegrationEvent : IntegrationEvent
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public DateTime BirthDate { get; private set; }
        public Gender Gender { get; private set; }
        public string Email { get; private set; }

        public FamiliarRegistradoIntegrationEvent(Guid id, string nome, DateTime birthDate, Gender gender, string email)
        {
            Id = id;
            Nome = nome;
            BirthDate = birthDate; 
            Gender = gender;
            Email = email;
        }
    }
}

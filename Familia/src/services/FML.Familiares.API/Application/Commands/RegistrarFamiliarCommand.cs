using FluentValidation;
using FluentValidation.Results;
using FML.Core.Messages;
using MediatR;

namespace FML.Familiares.API.Application.Commands
{
    [Serializable]
    public class RegistrarFamiliarCommand : Command
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public Gender Gender { get; private set; }

        public RegistrarFamiliarCommand(Guid id, string nome, string email, DateTime dataNascimento, Gender gender)
        {
            AggregateId = id;
            Id = id;
            Nome = nome;
            Email = email;
            BirthDate = dataNascimento;
            Gender = gender;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegistrarFamiliarValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class RegistrarFamiliarValidation : AbstractValidator<RegistrarFamiliarCommand>
        {
            public RegistrarFamiliarValidation()
            {
                RuleFor(c => c.Id)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id do familiar inválido");

                RuleFor(c => c.Nome)
                    .NotEmpty()
                    .WithMessage("O nome do familiar não foi informado");

                RuleFor(c => c.Email)
                    .NotEmpty()
                    .WithMessage("O e-mail do familiar não foi informado");

                RuleFor(c => c.Email)
                    .EmailAddress()
                    .WithMessage("O e-mail informado não é válido");

                RuleFor(c => c.BirthDate)
                    .NotEmpty()
                    .WithMessage("A data de nascimento do familiar não foi informada");

                RuleFor(c => c.Gender)
                    .IsInEnum()
                    .WithMessage("Gênero inválido");
            }
        }
    }

   
}

using FluentValidation.Results;
using FML.Core.Data;
using FML.Core.Messages;
using FML.Familiares.API.Application.Events;
using FML.Familiares.API.Data.Repository.Interface;
using MediatR;

namespace FML.Familiares.API.Application.Commands
{
    public class FamiliarCommandHandler : CommandHandler, 
        IRequestHandler<RegistrarFamiliarCommand, ValidationResult>
    {
        private readonly IRelativeRepository _relativeRepository;

        public FamiliarCommandHandler(IRelativeRepository relativeRepository)
        {
            _relativeRepository = relativeRepository;
        }

        public async Task<ValidationResult> Handle(RegistrarFamiliarCommand message, CancellationToken cancellationToken)
        {
            try
            {
                if (!message.IsValid()) return message.ValidationResult;

                var novoFamiliar = new Relative(message.Id, message.Nome, Constantes.FamiliaCarvalhoNome, message.Email, message.BirthDate, message.Gender);
                novoFamiliar.FamilyId = Constantes.FamiliaCarvalhoId;
                novoFamiliar.HouseId = Constantes.CasaCarvalhoId;

                var familiar = await _relativeRepository.GetRelativeById(message.Id);

                if (familiar != null)
                {
                    AddError("Família já adicionado");
                    return ValidationResult;
                }

                _relativeRepository.AddRelative(novoFamiliar);

                novoFamiliar.AdicionarEvento(new FamiliarRegistradoEvent(message.Id, message.Nome, message.Email, message.BirthDate, message.Gender));

                return await PersistirDados(_relativeRepository.UnitOfWork);
            }
            catch (Exception ex)
            {
                AddError(ex.Message);
                return ValidationResult;
            }
        }
    }
}

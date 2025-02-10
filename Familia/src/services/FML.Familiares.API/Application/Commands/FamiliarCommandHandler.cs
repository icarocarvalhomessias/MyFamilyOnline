using FluentValidation.Results;
using FML.Core.Data;
using FML.Core.Messages;
using FML.Familiares.API.Application.Events;
using FML.Familiares.API.Clients;
using FML.Familiares.API.Data.Repository.Interface;
using MediatR;

namespace FML.Familiares.API.Application.Commands
{
    public class FamiliarCommandHandler : CommandHandler, 
        IRequestHandler<RegistrarFamiliarCommand, ValidationResult>,
        IRequestHandler<AtualizarFamiliarCommand, ValidationResult>
    {
        private readonly IRelativeRepository _relativeRepository;
        private readonly IFileHttp _fileHttp;


        public FamiliarCommandHandler(IRelativeRepository relativeRepository, IFileHttp fileHttp)
        {
            _relativeRepository = relativeRepository;
            _fileHttp = fileHttp;
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

        public async Task<ValidationResult> Handle(AtualizarFamiliarCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingRelative = await _relativeRepository.GetRelativeById(request.Id);

                if (existingRelative == null)
                {
                    AddError("Familiar não encontrado");
                    return ValidationResult;
                }

                var updatedRelative = new Relative
                {
                    Id = request.Id,
                    FirstName = request.Nome,
                    LastName = request.Sobrenome,
                    FamilyId = request.Familia,
                    HouseId = request.Casa,
                    FatherId = request.Pai,
                    MotherId = request.Mae,
                    LinkName = request.NomeLink,
                    FotoStream = request.Foto,
                    SecretSanta = request.AmigoSecreto,
                    Email = request.Email,
                    BirthDate = request.DataNascimento,
                    IsActive = request.Ativo,
                    IsAlive = request.EstaVivo,
                    Patriarch = request.Patriarca,
                    Matriarch = request.Matriarca,
                    DeathDate = request.DataFalecimento,
                    Gender = Enum.Parse<Gender>(request.Genero),
                    FotoId = existingRelative.FotoId,
                    Spouse = request.SpouseId
                };

                if (!string.IsNullOrEmpty(request.FotoFileBase64))
                {
                    var bytes = Convert.FromBase64String(request.FotoFileBase64);
                    var fileName = $"{Guid.NewGuid()}.jpg";
                    var response = await _fileHttp.UploadPhotoAsync(bytes, fileName);

                    if (response.IsSuccessStatusCode)
                    {
                        var id = await response.Content.ReadAsStringAsync();
                        updatedRelative.FotoId = Guid.Parse(id.Trim('"'));
                    }
                }

                _relativeRepository.UpdateRelative(updatedRelative);

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

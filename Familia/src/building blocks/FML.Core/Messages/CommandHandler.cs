using FluentValidation.Results;
using FML.Core.Data;

namespace FML.Core.Messages
{
    public abstract class CommandHandler
    {
        protected ValidationResult ValidationResult { get; set; }

        protected CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }

        protected void AddError(string message)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, message));
        }

        protected async Task<ValidationResult> PersistirDados(IUnitOfWork uow)
        {
            if (!await uow.Commit()) AddError("Houve um erro ao persistir os dados");
            return ValidationResult;
        }
    }
}

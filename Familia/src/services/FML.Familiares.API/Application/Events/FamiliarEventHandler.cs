using MediatR;

namespace FML.Familiares.API.Application.Events
{
    public class FamiliarEventHandler : INotificationHandler<FamiliarRegistradoEvent>
    {
        public Task Handle(FamiliarRegistradoEvent notification, CancellationToken cancellationToken)
        {
            // enviar um evento de confirmação
            return Task.CompletedTask;
        }
    }
}

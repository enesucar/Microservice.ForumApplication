using Quesify.EventBus.Events;

namespace Quesify.AnswerService.API.IntegrationEvents.Events;

public class QuestionCreatedIntegrationEvent : IntegrationEvent
{
    public Guid QuestionId { get; set; }

    public QuestionCreatedIntegrationEvent(
        Guid id,
        DateTime creationDate)
        : base(id, creationDate)
    {
    }
}

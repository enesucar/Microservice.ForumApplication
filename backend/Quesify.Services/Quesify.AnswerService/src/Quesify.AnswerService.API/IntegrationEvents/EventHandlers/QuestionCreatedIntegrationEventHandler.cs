using Quesify.AnswerService.API.IntegrationEvents.Events;
using Quesify.AnswerService.Core.Aggregates.Questions;
using Quesify.AnswerService.Core.Interfaces;
using Quesify.SharedKernel.EventBus.Abstractions;
using Serilog.Context;

namespace Quesify.AnswerService.API.IntegrationEvents.EventHandlers;

public class QuestionCreatedIntegrationEventHandler : IIntegrationEventHandler<QuestionCreatedIntegrationEvent>
{
    private readonly IAnswerContext _context;
    private readonly ILogger<QuestionCreatedIntegrationEventHandler> _logger;

    public QuestionCreatedIntegrationEventHandler(
        IAnswerContext context,
        ILogger<QuestionCreatedIntegrationEventHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task HandleAsync(QuestionCreatedIntegrationEvent integrationEvent)
    {
        LogContext.PushProperty("EventId", integrationEvent.Id);
        _logger.Here().LogInformation("Handling integration event: {EventName} - ({@IntegrationEvent})", nameof(QuestionCreatedIntegrationEventHandler), integrationEvent);
        var question = new Question(integrationEvent.QuestionId);
        await _context.Questions.AddAsync(question);
        await _context.SaveChangesAsync();
    }
}

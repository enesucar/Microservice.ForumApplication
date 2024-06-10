using Quesify.SharedKernel.Core.Entities;

namespace Quesify.AnswerService.Core.Aggregates.Questions;

public class Question : AggregateRoot
{
    public Guid Id { get; private set; }

    private Question()
    {
    }

    public Question(Guid id)
    {
        Id = id;
    }
}

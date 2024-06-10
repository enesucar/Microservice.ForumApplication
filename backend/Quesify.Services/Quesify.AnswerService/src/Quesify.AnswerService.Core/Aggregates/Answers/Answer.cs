using Quesify.SharedKernel.Core.Entities;
using Quesify.SharedKernel.Utilities.Guards;

namespace Quesify.AnswerService.Core.Aggregates.Answers;

public class Answer : AggregateRoot
{
    public Guid Id { get; private set; }

    public Guid QuestionId { get; private set; }

    public string Body { get; private set; }

    public Guid UserId { get; private set; }

    public int Score { get; private set; }

    public DateTime CreationDate { get; private set; }

    public DateTime? ModificationDate { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Answer()
    {
    }

    internal Answer(
        Guid id,
        Guid questionId,
        string body,
        Guid userId,
        DateTime creationDate)
    {
        Id = id;
        QuestionId = questionId;
        SetBody(body);
        UserId = userId;
        Score = 0;
        CreationDate = creationDate;
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public void SetBody(string body)
    {
        Guard.Against.NullOrWhiteSpace(body, nameof(body));
        Body = body;
    }

    internal void IncreaseScore(int scoreToIncrease)
    {
        Guard.Against.NegativeOrZero(scoreToIncrease, nameof(scoreToIncrease));
        Score += scoreToIncrease;
    }

    internal void DecreaseScore(int scoreToDecrease)
    {
        Guard.Against.NegativeOrZero(scoreToDecrease, nameof(scoreToDecrease));
        Score -= scoreToDecrease;
    }
}

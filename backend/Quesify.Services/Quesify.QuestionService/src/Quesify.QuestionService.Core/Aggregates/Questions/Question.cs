using Quesify.SharedKernel.Core.Entities;
using Quesify.SharedKernel.Utilities.Guards;

namespace Quesify.QuestionService.Core.Aggregates.Questions;

public class Question : AggregateRoot
{
    public Guid Id { get; private set; }

    public string Title { get; private set; }

    public string Body { get; private set; }

    public Guid UserId { get; private set; }

    public int Score { get; private set; }

    public DateTime CreationDate { get; private set; }

    public DateTime? ModificationDate { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Question()
    {
    }

    internal Question(
        Guid id,
        string title,
        string body,
        Guid userId,
        DateTime creationDate)
    {
        Id = id;
        SetTitle(title);
        SetBody(body);
        UserId = userId;
        CreationDate = creationDate;
        ModificationDate = null;
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    internal void SetTitle(string title)
    {
        Guard.Against.NullOrWhiteSpace(title, nameof(title), QuestionConstants.MaxTitleLength);
        Title = title;
    }

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

using Quesify.SharedKernel.Core.Entities;

namespace Quesify.QuestionService.Core.Aggregates.Votes;

public class Vote : AggregateRoot
{
    public Guid QuestionId { get; private set; }

    public Guid UserId { get; private set; }

    public VoteType VoteType { get; private set; }

    public DateTime CreationDate { get; private set; }

    private Vote()
    {
    }

    internal Vote(
        Guid questionId,
        Guid userId,
        VoteType voteType,
        DateTime creationDate)
    {
        QuestionId = questionId;
        UserId = userId;
        VoteType = voteType;
        CreationDate = creationDate;
    }
}

using Quesify.SharedKernel.Core.Entities;

namespace Quesify.AnswerService.Core.Aggregates.Votes;

public class Vote : AggregateRoot
{
    public Guid AnswerId { get; private set; }

    public Guid UserId { get; private set; }

    public VoteType VoteType { get; private set; }

    public DateTime CreationDate { get; private set; }

    private Vote()
    {
    }

    internal Vote(
        Guid answerId,
        Guid userId,
        VoteType voteType,
        DateTime creationDate)
    {
        AnswerId = answerId;
        UserId = userId;
        VoteType = voteType;
        CreationDate = creationDate;
    }
}

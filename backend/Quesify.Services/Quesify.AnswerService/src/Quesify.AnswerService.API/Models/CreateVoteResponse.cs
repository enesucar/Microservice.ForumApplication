using Quesify.AnswerService.Core.Aggregates.Votes;

namespace Quesify.AnswerService.API.Models;

public class CreateVoteResponse
{
    public Guid QuestionId { get; set; }

    public Guid AnswerId { get; set; }

    public VoteType VoteType { get; set; }

    public int NewAnswerScore { get; set; }
}

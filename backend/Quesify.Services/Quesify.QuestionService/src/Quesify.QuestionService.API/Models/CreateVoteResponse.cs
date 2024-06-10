using Quesify.QuestionService.Core.Aggregates.Votes;

namespace Quesify.QuestionService.API.Models;

public class CreateVoteResponse
{
    public Guid QuestionId { get; set; }

    public VoteType VoteType { get; set; }

    public int NewQuestionScore { get; set; }
}

using Quesify.QuestionService.Core.Aggregates.Votes;

namespace Quesify.QuestionService.API.Models;

public class CreateVoteRequest
{
    public VoteType VoteType { get; set; }
}

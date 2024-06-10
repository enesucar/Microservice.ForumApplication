using Quesify.AnswerService.Core.Aggregates.Votes;

namespace Quesify.AnswerService.API.Models;

public class CreateVoteRequest
{
    public VoteType VoteType { get; set; }
}

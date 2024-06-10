using Quesify.AnswerService.Core.Aggregates.Answers;
using Quesify.AnswerService.Core.Aggregates.Votes;

namespace Quesify.AnswerService.Core.Interfaces;

public interface IVoteService
{
    Task<Vote> CreateAsync(Answer answer, Guid userId, VoteType voteType);
}

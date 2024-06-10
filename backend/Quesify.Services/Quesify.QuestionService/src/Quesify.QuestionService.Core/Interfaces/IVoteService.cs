using Quesify.QuestionService.Core.Aggregates.Questions;
using Quesify.QuestionService.Core.Aggregates.Votes;

namespace Quesify.QuestionService.Core.Interfaces;

public interface IVoteService
{
    Task<Vote> CreateAsync(Question question, Guid userId, VoteType voteType);
}

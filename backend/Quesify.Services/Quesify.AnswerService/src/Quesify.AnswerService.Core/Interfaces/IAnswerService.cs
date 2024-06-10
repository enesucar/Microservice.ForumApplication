using Quesify.AnswerService.Core.Aggregates.Answers;
using Quesify.AnswerService.Core.Aggregates.Questions;
using Quesify.AnswerService.Core.Aggregates.Votes;

namespace Quesify.AnswerService.Core.Interfaces;

public interface IAnswerService
{
    Task<Answer> CreateAsync(Question question, string body, Guid userId);

    Task<int> SetScoreAsync(Answer answer, Vote vote);
}

using Quesify.QuestionService.Core.Aggregates.Questions;
using Quesify.QuestionService.Core.Aggregates.Votes;

namespace Quesify.QuestionService.Core.Interfaces;

public interface IQuestionService
{
    Task<Question> CreateAsync(string title, string body, Guid userId);

    Task<int> SetScoreAsync(Question question, Vote vote);
}

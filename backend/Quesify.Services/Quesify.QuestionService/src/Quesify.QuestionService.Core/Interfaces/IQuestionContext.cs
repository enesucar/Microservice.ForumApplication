using Microsoft.EntityFrameworkCore;
using Quesify.QuestionService.Core.Aggregates.Questions;
using Quesify.QuestionService.Core.Aggregates.Votes;

namespace Quesify.QuestionService.Core.Interfaces;

public interface IQuestionContext
{
    DbSet<Question> Questions { get; }

    DbSet<Vote> Votes { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

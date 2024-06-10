using Microsoft.EntityFrameworkCore;
using Quesify.AnswerService.Core.Aggregates.Answers;
using Quesify.AnswerService.Core.Aggregates.Questions;
using Quesify.AnswerService.Core.Aggregates.Votes;

namespace Quesify.AnswerService.Core.Interfaces;

public interface IAnswerContext
{
    DbSet<Question> Questions { get; }

    DbSet<Answer> Answers { get; }

    DbSet<Vote> Votes { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

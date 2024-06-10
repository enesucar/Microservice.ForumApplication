using Microsoft.EntityFrameworkCore;
using Quesify.QuestionService.Core.Aggregates.Questions;
using Quesify.QuestionService.Core.Aggregates.Votes;
using Quesify.QuestionService.Core.Interfaces;
using System.Reflection;

namespace Quesify.QuestionService.Infrastructure.Data.Contexts;

public class QuestionContext : DbContext, IQuestionContext
{
    public DbSet<Question> Questions => Set<Question>();

    public DbSet<Vote> Votes => Set<Vote>();

    public QuestionContext(DbContextOptions<QuestionContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}

using Microsoft.EntityFrameworkCore;
using Quesify.AnswerService.Core.Aggregates.Answers;
using Quesify.AnswerService.Core.Aggregates.Questions;
using Quesify.AnswerService.Core.Aggregates.Votes;
using Quesify.AnswerService.Core.Interfaces;
using System.Reflection;

namespace Quesify.AnswerService.Infrastructure.Data.Contexts;

public class AnswerContext : DbContext, IAnswerContext
{
    public DbSet<Question> Questions => Set<Question>();

    public DbSet<Answer> Answers => Set<Answer>();

    public DbSet<Vote> Votes => Set<Vote>();

    public AnswerContext(DbContextOptions<AnswerContext> options) : base(options) { }

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

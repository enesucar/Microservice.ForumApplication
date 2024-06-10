using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quesify.AnswerService.Core.Aggregates.Answers;
using Quesify.AnswerService.Core.Aggregates.Questions;

namespace Quesify.AnswerService.Infrastructure.Data.Configurations;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable("questions").HasKey(o => o.Id);
        builder.Property(p => p.Id).HasColumnName("id").IsRequired();
        builder.HasMany<Answer>().WithOne().HasForeignKey(o => o.QuestionId);
    }
}

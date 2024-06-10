using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quesify.AnswerService.Core.Aggregates.Answers;
using Quesify.AnswerService.Core.Aggregates.Questions;

namespace Quesify.AnswerService.Infrastructure.Data.Configurations;

public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
{
    public void Configure(EntityTypeBuilder<Answer> builder)
    {
        builder.ToTable("answers").HasKey(o => o.Id);
        builder.Property(p => p.Id).HasColumnName("id").IsRequired();
        builder.Property(p => p.QuestionId).HasColumnName("question_id").IsRequired();
        builder.Property(p => p.Body).HasColumnName("body").IsRequired();
        builder.Property(p => p.UserId).HasColumnName("user_id").IsRequired();
        builder.Property(p => p.Score).HasColumnName("score").IsRequired();
        builder.Property(p => p.CreationDate).HasColumnName("creation_date").IsRequired();
        builder.Property(p => p.ModificationDate).HasColumnName("modification_date");
        builder.HasOne<Question>().WithMany().HasForeignKey(o => o.QuestionId);
    }
}

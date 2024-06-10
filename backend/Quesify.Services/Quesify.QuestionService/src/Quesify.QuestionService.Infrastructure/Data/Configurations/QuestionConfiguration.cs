using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quesify.QuestionService.Core.Aggregates.Questions;
using Quesify.QuestionService.Core.Aggregates.Votes;

namespace Quesify.QuestionService.Infrastructure.Data.Configurations;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable("questions").HasKey(o => o.Id);
        builder.Property(p => p.Id).HasColumnName("id").HasColumnType("varchar(36)").IsRequired();
        builder.Property(p => p.Title).HasColumnName("title").HasMaxLength(QuestionConstants.MaxTitleLength).IsRequired();
        builder.Property(p => p.Body).HasColumnName("body").IsRequired();
        builder.Property(p => p.UserId).HasColumnName("user_id").HasColumnType("varchar(36)").IsRequired();
        builder.Property(p => p.Score).HasColumnName("score").IsRequired();
        builder.Property(p => p.CreationDate).HasColumnName("creation_date").HasColumnType("datetime").IsRequired();
        builder.Property(p => p.ModificationDate).HasColumnName("modification_date").HasColumnType("datetime");
        builder.HasIndex(p => p.Title).IsUnique();
        builder.HasMany<Vote>().WithOne().HasForeignKey(o => o.QuestionId).OnDelete(DeleteBehavior.NoAction);
    }
}

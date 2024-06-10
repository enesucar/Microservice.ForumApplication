using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quesify.QuestionService.Core.Aggregates.Questions;
using Quesify.QuestionService.Core.Aggregates.Votes;

namespace Quesify.QuestionService.Infrastructure.Data.Configurations;

public class VoteConfiguration : IEntityTypeConfiguration<Vote>
{
    public void Configure(EntityTypeBuilder<Vote> builder)
    {
        builder.ToTable("votes").HasKey(o => new { o.QuestionId, o.UserId });
        builder.Property(p => p.QuestionId).HasColumnName("question_id").HasColumnType("varchar(36)").IsRequired();
        builder.Property(p => p.UserId).HasColumnName("user_id").HasColumnType("varchar(36)").IsRequired();
        builder.Property(p => p.VoteType).HasColumnName("vote_type").HasColumnType("tinyint").IsRequired();
        builder.Property(p => p.CreationDate).HasColumnName("creation_date").HasColumnType("datetime").IsRequired();
        builder.HasOne<Question>().WithMany().HasForeignKey(o => o.QuestionId).OnDelete(DeleteBehavior.NoAction);
    }
}

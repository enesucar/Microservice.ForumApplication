using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quesify.AnswerService.Core.Aggregates.Answers;
using Quesify.AnswerService.Core.Aggregates.Votes;

namespace Quesify.AnswerService.Infrastructure.Data.Configurations;

public class VoteConfiguration : IEntityTypeConfiguration<Vote>
{
    public void Configure(EntityTypeBuilder<Vote> builder)
    {
        builder.ToTable("votes").HasKey(o => new { o.AnswerId, o.UserId });
        builder.Property(p => p.AnswerId).HasColumnName("answer_id").IsRequired();
        builder.Property(p => p.UserId).HasColumnName("user_id").IsRequired();
        builder.Property(p => p.VoteType).HasColumnName("vote_type").IsRequired();
        builder.Property(p => p.CreationDate).HasColumnName("creation_date").IsRequired();
        builder.HasOne<Answer>().WithMany().HasForeignKey(o => o.AnswerId).OnDelete(DeleteBehavior.NoAction);
    }
}

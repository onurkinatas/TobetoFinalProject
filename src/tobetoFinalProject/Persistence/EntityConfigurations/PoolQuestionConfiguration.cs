using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class PoolQuestionConfiguration : IEntityTypeConfiguration<PoolQuestion>
{
    public void Configure(EntityTypeBuilder<PoolQuestion> builder)
    {
        builder.ToTable("PoolQuestions").HasKey(pq => pq.Id);

        builder.Property(pq => pq.Id).HasColumnName("Id").IsRequired();
        builder.Property(pq => pq.PoolId).HasColumnName("PoolId");
        builder.Property(pq => pq.QuestionId).HasColumnName("QuestionId");
        builder.Property(pq => pq.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(pq => pq.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(pq => pq.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(pq => !pq.DeletedDate.HasValue);
    }
}
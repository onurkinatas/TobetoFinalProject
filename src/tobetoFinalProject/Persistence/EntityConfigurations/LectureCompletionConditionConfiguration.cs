using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class LectureCompletionConditionConfiguration : IEntityTypeConfiguration<LectureCompletionCondition>
{
    public void Configure(EntityTypeBuilder<LectureCompletionCondition> builder)
    {
        builder.ToTable("LectureCompletionConditions").HasKey(lcc => lcc.Id);

        builder.Property(lcc => lcc.Id).HasColumnName("Id").IsRequired();
        builder.Property(lcc => lcc.StudentId).HasColumnName("StudentId");
        builder.Property(lcc => lcc.LectureId).HasColumnName("LectureId");
        builder.Property(lcc => lcc.CompletionPercentage).HasColumnName("CompletionPercentage");
        builder.Property(lcc => lcc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(lcc => lcc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(lcc => lcc.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(lcc => !lcc.DeletedDate.HasValue);
    }
}
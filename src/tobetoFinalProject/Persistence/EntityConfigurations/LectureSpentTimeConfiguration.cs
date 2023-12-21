using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class LectureSpentTimeConfiguration : IEntityTypeConfiguration<LectureSpentTime>
{
    public void Configure(EntityTypeBuilder<LectureSpentTime> builder)
    {
        builder.ToTable("LectureSpentTimes").HasKey(lst => lst.Id);

        builder.Property(lst => lst.Id).HasColumnName("Id").IsRequired();
        builder.Property(lst => lst.StudentId).HasColumnName("StudentId");
        builder.Property(lst => lst.LectureId).HasColumnName("LectureId");
        builder.Property(lst => lst.SpentedTime).HasColumnName("SpentedTime");
        builder.Property(lst => lst.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(lst => lst.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(lst => lst.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(lst => !lst.DeletedDate.HasValue);
    }
}
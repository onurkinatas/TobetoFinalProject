using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class LectureViewConfiguration : IEntityTypeConfiguration<LectureView>
{
    public void Configure(EntityTypeBuilder<LectureView> builder)
    {
        builder.ToTable("LectureViews").HasKey(lv => lv.Id);

        builder.Property(lv => lv.Id).HasColumnName("Id").IsRequired();
        builder.Property(lv => lv.StudentId).HasColumnName("StudentId");
        builder.Property(lv => lv.LectureId).HasColumnName("LectureId");
        builder.Property(lv => lv.ContentId).HasColumnName("ContentId");
        builder.Property(lv => lv.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(lv => lv.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(lv => lv.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(lv => !lv.DeletedDate.HasValue);
    }
}
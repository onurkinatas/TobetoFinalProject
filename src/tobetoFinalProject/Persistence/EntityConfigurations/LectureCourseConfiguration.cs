using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class LectureCourseConfiguration : IEntityTypeConfiguration<LectureCourse>
{
    public void Configure(EntityTypeBuilder<LectureCourse> builder)
    {
        builder.ToTable("LectureCourses").HasKey(lc => lc.Id);

        builder.Property(lc => lc.Id).HasColumnName("Id").IsRequired();
        builder.Property(lc => lc.CourseId).HasColumnName("CourseId");
        builder.Property(lc => lc.LectureId).HasColumnName("LectureId");
        builder.Property(lc => lc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(lc => lc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(lc => lc.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(lc => !lc.DeletedDate.HasValue);
    }
}
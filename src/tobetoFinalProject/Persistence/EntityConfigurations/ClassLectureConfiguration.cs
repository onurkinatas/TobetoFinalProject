using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ClassLectureConfiguration : IEntityTypeConfiguration<ClassLecture>
{
    public void Configure(EntityTypeBuilder<ClassLecture> builder)
    {
        builder.ToTable("ClassLectures").HasKey(cl => cl.Id);

        builder.Property(cl => cl.Id).HasColumnName("Id").IsRequired();
        builder.Property(cl => cl.LectureId).HasColumnName("LectureId");
        builder.Property(cl => cl.StudentClassId).HasColumnName("StudentClassId");
        builder.Property(cl => cl.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(cl => cl.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(cl => cl.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(cl => !cl.DeletedDate.HasValue);
    }
}
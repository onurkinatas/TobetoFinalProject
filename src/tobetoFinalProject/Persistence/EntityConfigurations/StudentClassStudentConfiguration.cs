using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class StudentClassStudentConfiguration : IEntityTypeConfiguration<StudentClassStudent>
{
    public void Configure(EntityTypeBuilder<StudentClassStudent> builder)
    {
        builder.ToTable("StudentClassStudents").HasKey(scs => scs.Id);

        builder.Property(scs => scs.Id).HasColumnName("Id").IsRequired();
        builder.Property(scs => scs.StudentId).HasColumnName("StudentId");
        builder.Property(scs => scs.StudentClassId).HasColumnName("StudentClassId");
        builder.Property(scs => scs.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(scs => scs.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(scs => scs.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(scs => !scs.DeletedDate.HasValue);
    }
}
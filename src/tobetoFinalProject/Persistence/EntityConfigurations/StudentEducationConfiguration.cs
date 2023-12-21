using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class StudentEducationConfiguration : IEntityTypeConfiguration<StudentEducation>
{
    public void Configure(EntityTypeBuilder<StudentEducation> builder)
    {
        builder.ToTable("StudentEducations").HasKey(se => se.Id);

        builder.Property(se => se.Id).HasColumnName("Id").IsRequired();
        builder.Property(se => se.StudentId).HasColumnName("StudentId");
        builder.Property(se => se.EducationStatus).HasColumnName("EducationStatus");
        builder.Property(se => se.SchoolName).HasColumnName("SchoolName");
        builder.Property(se => se.Branch).HasColumnName("Branch");
        builder.Property(se => se.IsContinued).HasColumnName("IsContinued");
        builder.Property(se => se.StartDate).HasColumnName("StartDate");
        builder.Property(se => se.GraduationDate).HasColumnName("GraduationDate");
        builder.Property(se => se.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(se => se.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(se => se.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(se => !se.DeletedDate.HasValue);
    }
}
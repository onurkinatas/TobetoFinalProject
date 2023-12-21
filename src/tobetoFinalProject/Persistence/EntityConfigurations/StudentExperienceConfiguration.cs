using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class StudentExperienceConfiguration : IEntityTypeConfiguration<StudentExperience>
{
    public void Configure(EntityTypeBuilder<StudentExperience> builder)
    {
        builder.ToTable("StudentExperiences").HasKey(se => se.Id);

        builder.Property(se => se.Id).HasColumnName("Id").IsRequired();
        builder.Property(se => se.StudentId).HasColumnName("StudentId");
        builder.Property(se => se.CompanyName).HasColumnName("CompanyName");
        builder.Property(se => se.Sector).HasColumnName("Sector");
        builder.Property(se => se.Position).HasColumnName("Position");
        builder.Property(se => se.StartDate).HasColumnName("StartDate");
        builder.Property(se => se.EndDate).HasColumnName("EndDate");
        builder.Property(se => se.Description).HasColumnName("Description");
        builder.Property(se => se.CityId).HasColumnName("CityId");
        builder.Property(se => se.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(se => se.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(se => se.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(se => !se.DeletedDate.HasValue);
    }
}
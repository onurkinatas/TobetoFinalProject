using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ClassSurveyConfiguration : IEntityTypeConfiguration<ClassSurvey>
{
    public void Configure(EntityTypeBuilder<ClassSurvey> builder)
    {
        builder.ToTable("ClassSurveys").HasKey(cs => cs.Id);

        builder.Property(cs => cs.Id).HasColumnName("Id").IsRequired();
        builder.Property(cs => cs.StudentClassId).HasColumnName("StudentClassId");
        builder.Property(cs => cs.SurveyId).HasColumnName("SurveyId");
        builder.Property(cs => cs.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(cs => cs.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(cs => cs.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(cs => !cs.DeletedDate.HasValue);
    }
}
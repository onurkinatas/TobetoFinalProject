using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class StudentLanguageLevelConfiguration : IEntityTypeConfiguration<StudentLanguageLevel>
{
    public void Configure(EntityTypeBuilder<StudentLanguageLevel> builder)
    {
        builder.ToTable("StudentLanguageLevels").HasKey(sll => sll.Id);

        builder.Property(sll => sll.Id).HasColumnName("Id").IsRequired();
        builder.Property(sll => sll.StudentId).HasColumnName("StudentId");
        builder.Property(sll => sll.LanguageLevelId).HasColumnName("LanguageLevelId");
        builder.Property(sll => sll.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(sll => sll.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(sll => sll.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(sll => !sll.DeletedDate.HasValue);
    }
}
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ContentInstructorConfiguration : IEntityTypeConfiguration<ContentInstructor>
{
    public void Configure(EntityTypeBuilder<ContentInstructor> builder)
    {
        builder.ToTable("ContentInstructors").HasKey(ci => ci.Id);

        builder.Property(ci => ci.Id).HasColumnName("Id").IsRequired();
        builder.Property(ci => ci.ContentId).HasColumnName("ContentId");
        builder.Property(ci => ci.InstructorId).HasColumnName("InstructorId");
        builder.Property(ci => ci.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(ci => ci.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(ci => ci.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(ci => !ci.DeletedDate.HasValue);
    }
}
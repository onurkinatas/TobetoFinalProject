using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class LectureConfiguration : IEntityTypeConfiguration<Lecture>
{
    public void Configure(EntityTypeBuilder<Lecture> builder)
    {
        builder.ToTable("Lectures").HasKey(l => l.Id);

        builder.Property(l => l.Id).HasColumnName("Id").IsRequired();
        builder.Property(l => l.Name).HasColumnName("Name");
        builder.Property(l => l.CategoryId).HasColumnName("CategoryId");
        builder.Property(l => l.ImageUrl).HasColumnName("ImageUrl");
        builder.Property(l => l.EstimatedDuration).HasColumnName("EstimatedDuration");
        builder.Property(l => l.ManufacturerId).HasColumnName("ManufacturerId");
        builder.Property(l => l.StartDate).HasColumnName("StartDate");
        builder.Property(l => l.EndDate).HasColumnName("EndDate");
        builder.Property(l => l.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(l => l.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(l => l.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(l => !l.DeletedDate.HasValue);
    }
}
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ContentConfiguration : IEntityTypeConfiguration<Content>
{
    public void Configure(EntityTypeBuilder<Content> builder)
    {
        builder.ToTable("Contents").HasKey(c => c.Id);

        builder.Property(c => c.Id).HasColumnName("Id").IsRequired();
        builder.Property(c => c.Name).HasColumnName("Name");
        builder.Property(c => c.LanguageId).HasColumnName("LanguageId");
        builder.Property(c => c.SubTypeId).HasColumnName("SubTypeId");
        builder.Property(c => c.VideoUrl).HasColumnName("VideoUrl");
        builder.Property(c => c.Description).HasColumnName("Description");
        builder.Property(c => c.ManufacturerId).HasColumnName("ManufacturerId");
        builder.Property(c => c.Duration).HasColumnName("Duration");
        builder.Property(c => c.ContentCategoryId).HasColumnName("ContentCategoryId");
        builder.Property(c => c.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(c => c.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(c => c.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(c => !c.DeletedDate.HasValue);
    }
}
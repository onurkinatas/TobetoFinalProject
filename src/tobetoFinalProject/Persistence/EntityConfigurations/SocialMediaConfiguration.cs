using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class SocialMediaConfiguration : IEntityTypeConfiguration<SocialMedia>
{
    public void Configure(EntityTypeBuilder<SocialMedia> builder)
    {
        builder.ToTable("SocialMedias").HasKey(sm => sm.Id);

        builder.Property(sm => sm.Id).HasColumnName("Id").IsRequired();
        builder.Property(sm => sm.Name).HasColumnName("Name");
        builder.Property(sm => sm.LogoUrl).HasColumnName("LogoUrl");
        builder.Property(sm => sm.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(sm => sm.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(sm => sm.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(sm => !sm.DeletedDate.HasValue);
    }
}
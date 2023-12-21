using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ContentLikeConfiguration : IEntityTypeConfiguration<ContentLike>
{
    public void Configure(EntityTypeBuilder<ContentLike> builder)
    {
        builder.ToTable("ContentLikes").HasKey(cl => cl.Id);

        builder.Property(cl => cl.Id).HasColumnName("Id").IsRequired();
        builder.Property(cl => cl.IsLiked).HasColumnName("IsLiked");
        builder.Property(cl => cl.StudentId).HasColumnName("StudentId");
        builder.Property(cl => cl.ContentId).HasColumnName("ContentId");
        builder.Property(cl => cl.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(cl => cl.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(cl => cl.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(cl => !cl.DeletedDate.HasValue);
    }
}
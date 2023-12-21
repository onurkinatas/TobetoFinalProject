using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class LectureLikeConfiguration : IEntityTypeConfiguration<LectureLike>
{
    public void Configure(EntityTypeBuilder<LectureLike> builder)
    {
        builder.ToTable("LectureLikes").HasKey(ll => ll.Id);

        builder.Property(ll => ll.Id).HasColumnName("Id").IsRequired();
        builder.Property(ll => ll.IsLiked).HasColumnName("IsLiked");
        builder.Property(ll => ll.StudentId).HasColumnName("StudentId");
        builder.Property(ll => ll.LectureId).HasColumnName("LectureId");
        builder.Property(ll => ll.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(ll => ll.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(ll => ll.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(ll => !ll.DeletedDate.HasValue);
    }
}
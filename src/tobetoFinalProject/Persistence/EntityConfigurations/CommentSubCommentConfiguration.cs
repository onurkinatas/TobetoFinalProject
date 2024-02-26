using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class CommentSubCommentConfiguration : IEntityTypeConfiguration<CommentSubComment>
{
    public void Configure(EntityTypeBuilder<CommentSubComment> builder)
    {
        builder.ToTable("CommentSubComments").HasKey(csc => csc.Id);

        builder.Property(csc => csc.Id).HasColumnName("Id").IsRequired();
        builder.Property(csc => csc.StudentLectureCommentId).HasColumnName("StudentLectureCommentId");
        builder.Property(csc => csc.StudentId).HasColumnName("StudentId");
        builder.Property(csc => csc.SubComment).HasColumnName("SubComment");
        builder.Property(csc => csc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(csc => csc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(csc => csc.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(csc => !csc.DeletedDate.HasValue);
    }
}
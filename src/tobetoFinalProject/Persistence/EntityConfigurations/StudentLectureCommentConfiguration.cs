using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class StudentLectureCommentConfiguration : IEntityTypeConfiguration<StudentLectureComment>
{
    public void Configure(EntityTypeBuilder<StudentLectureComment> builder)
    {
        builder.ToTable("StudentLectureComments").HasKey(slc => slc.Id);

        builder.Property(slc => slc.Id).HasColumnName("Id").IsRequired();
        builder.Property(slc => slc.LectureId).HasColumnName("LectureId");
        builder.Property(slc => slc.StudentId).HasColumnName("StudentId");
        builder.Property(slc => slc.Comment).HasColumnName("Comment");
        builder.Property(slc => slc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(slc => slc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(slc => slc.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(slc => !slc.DeletedDate.HasValue);
    }
}
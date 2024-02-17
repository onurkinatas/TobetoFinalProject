using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class StudentQuizResultConfiguration : IEntityTypeConfiguration<StudentQuizResult>
{
    public void Configure(EntityTypeBuilder<StudentQuizResult> builder)
    {
        builder.ToTable("StudentQuizResults").HasKey(sqr => sqr.Id);

        builder.Property(sqr => sqr.Id).HasColumnName("Id").IsRequired();
        builder.Property(sqr => sqr.StudentId).HasColumnName("StudentId");
        builder.Property(sqr => sqr.QuizId).HasColumnName("QuizId");
        builder.Property(sqr => sqr.CorrectAnswerCount).HasColumnName("CorrectAnswerCount");
        builder.Property(sqr => sqr.WrongAnswerCount).HasColumnName("WrongAnswerCount");
        builder.Property(sqr => sqr.EmptyAnswerCount).HasColumnName("EmptyAnswerCount");
       
        builder.Property(sqr => sqr.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(sqr => sqr.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(sqr => sqr.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(sqr => !sqr.DeletedDate.HasValue);
    }
}
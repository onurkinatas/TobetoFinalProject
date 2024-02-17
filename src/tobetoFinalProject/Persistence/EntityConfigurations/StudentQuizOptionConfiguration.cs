using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class StudentQuizOptionConfiguration : IEntityTypeConfiguration<StudentQuizOption>
{
    public void Configure(EntityTypeBuilder<StudentQuizOption> builder)
    {
        builder.ToTable("StudentQuizOptions").HasKey(sqo => sqo.Id);

        builder.Property(sqo => sqo.Id).HasColumnName("Id").IsRequired();
        builder.Property(sqo => sqo.QuizId).HasColumnName("QuizId");
        builder.Property(sqo => sqo.QuestionId).HasColumnName("QuestionId");
        builder.Property(sqo => sqo.OptionId).HasColumnName("OptionId");
        builder.Property(sqo => sqo.StudentId).HasColumnName("StudentId");
        builder.Property(sqo => sqo.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(sqo => sqo.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(sqo => sqo.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(sqo => !sqo.DeletedDate.HasValue);
    }
}
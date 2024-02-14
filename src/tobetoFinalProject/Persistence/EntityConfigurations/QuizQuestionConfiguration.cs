using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class QuizQuestionConfiguration : IEntityTypeConfiguration<QuizQuestion>
{
    public void Configure(EntityTypeBuilder<QuizQuestion> builder)
    {
        builder.ToTable("QuizQuestions").HasKey(qq => qq.Id);

        builder.Property(qq => qq.Id).HasColumnName("Id").IsRequired();
        builder.Property(qq => qq.QuizId).HasColumnName("QuizId");
        builder.Property(qq => qq.QuestionId).HasColumnName("QuestionId");
        builder.Property(qq => qq.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(qq => qq.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(qq => qq.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(qq => !qq.DeletedDate.HasValue);
    }
}
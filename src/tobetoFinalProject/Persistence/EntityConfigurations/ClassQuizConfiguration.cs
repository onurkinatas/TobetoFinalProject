using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ClassQuizConfiguration : IEntityTypeConfiguration<ClassQuiz>
{
    public void Configure(EntityTypeBuilder<ClassQuiz> builder)
    {
        builder.ToTable("ClassQuizs").HasKey(cq => cq.Id);

        builder.Property(cq => cq.Id).HasColumnName("Id").IsRequired();
        builder.Property(cq => cq.StudentClassId).HasColumnName("StudentClassId");
        builder.Property(cq => cq.QuizId).HasColumnName("QuizId");
        builder.Property(cq => cq.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(cq => cq.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(cq => cq.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(cq => !cq.DeletedDate.HasValue);
    }
}
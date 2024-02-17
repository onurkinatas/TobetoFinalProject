using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class GeneralQuizConfiguration : IEntityTypeConfiguration<GeneralQuiz>
{
    public void Configure(EntityTypeBuilder<GeneralQuiz> builder)
    {
        builder.ToTable("GeneralQuizs").HasKey(gq => gq.Id);

        builder.Property(gq => gq.Id).HasColumnName("Id").IsRequired();
        builder.Property(gq => gq.QuizId).HasColumnName("QuizId");
        builder.Property(gq => gq.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(gq => gq.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(gq => gq.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(gq => !gq.DeletedDate.HasValue);
    }
}
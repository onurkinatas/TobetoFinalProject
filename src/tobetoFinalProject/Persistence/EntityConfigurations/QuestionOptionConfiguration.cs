using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class QuestionOptionConfiguration : IEntityTypeConfiguration<QuestionOption>
{
    public void Configure(EntityTypeBuilder<QuestionOption> builder)
    {
        builder.ToTable("QuestionOptions").HasKey(qo => qo.Id);

        builder.Property(qo => qo.Id).HasColumnName("Id").IsRequired();
        builder.Property(qo => qo.OptionId).HasColumnName("OptionId");
        builder.Property(qo => qo.QuestionId).HasColumnName("QuestionId");
        builder.Property(qo => qo.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(qo => qo.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(qo => qo.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(qo => !qo.DeletedDate.HasValue);
    }
}
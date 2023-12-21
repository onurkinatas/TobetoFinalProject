using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class AppealStageConfiguration : IEntityTypeConfiguration<AppealStage>
{
    public void Configure(EntityTypeBuilder<AppealStage> builder)
    {
        builder.ToTable("AppealStages").HasKey(asd => asd.Id);

        builder.Property(asd => asd.Id).HasColumnName("Id").IsRequired();
        builder.Property(asd => asd.AppealId).HasColumnName("AppealId");
        builder.Property(asd => asd.StageId).HasColumnName("StageId");
        builder.Property(asd => asd.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(asd => asd.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(asd => asd.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(asd => !asd.DeletedDate.HasValue);
    }
}
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ClassAnnouncementConfiguration : IEntityTypeConfiguration<ClassAnnouncement>
{
    public void Configure(EntityTypeBuilder<ClassAnnouncement> builder)
    {
        builder.ToTable("ClassAnnouncements").HasKey(ca => ca.Id);

        builder.Property(ca => ca.Id).HasColumnName("Id").IsRequired();
        builder.Property(ca => ca.AnnouncementId).HasColumnName("AnnouncementId");
        builder.Property(ca => ca.StudentClassId).HasColumnName("StudentClassId");
        builder.Property(ca => ca.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(ca => ca.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(ca => ca.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(ca => !ca.DeletedDate.HasValue);
    }
}
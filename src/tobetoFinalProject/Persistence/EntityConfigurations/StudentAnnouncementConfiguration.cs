using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class StudentAnnouncementConfiguration : IEntityTypeConfiguration<StudentAnnouncement>
{
    public void Configure(EntityTypeBuilder<StudentAnnouncement> builder)
    {
        builder.ToTable("StudentAnnouncements").HasKey(sa => sa.Id);

        builder.Property(sa => sa.Id).HasColumnName("Id").IsRequired();
        builder.Property(sa => sa.AnnouncementId).HasColumnName("AnnouncementId");
        builder.Property(sa => sa.StudentId).HasColumnName("StudentId");
        builder.Property(sa => sa.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(sa => sa.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(sa => sa.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(sa => !sa.DeletedDate.HasValue);
    }
}
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class StudentCertificateConfiguration : IEntityTypeConfiguration<StudentCertificate>
{
    public void Configure(EntityTypeBuilder<StudentCertificate> builder)
    {
        builder.ToTable("StudentCertificates").HasKey(sc => sc.Id);

        builder.Property(sc => sc.Id).HasColumnName("Id").IsRequired();
        builder.Property(sc => sc.StudentId).HasColumnName("StudentId");
        builder.Property(sc => sc.CertificateId).HasColumnName("CertificateId");
        builder.Property(sc => sc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(sc => sc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(sc => sc.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(sc => !sc.DeletedDate.HasValue);
    }
}
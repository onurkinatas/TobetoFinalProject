using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class StudentPrivateCertificateConfiguration : IEntityTypeConfiguration<StudentPrivateCertificate>
{
    public void Configure(EntityTypeBuilder<StudentPrivateCertificate> builder)
    {
        builder.ToTable("StudentPrivateCertificates").HasKey(spc => spc.Id);

        builder.Property(spc => spc.Id).HasColumnName("Id").IsRequired();
        builder.Property(spc => spc.StudentId).HasColumnName("StudentId");
        builder.Property(spc => spc.CertificateUrl).HasColumnName("CertificateUrl");
        builder.Property(spc => spc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(spc => spc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(spc => spc.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(spc => !spc.DeletedDate.HasValue);
    }
}
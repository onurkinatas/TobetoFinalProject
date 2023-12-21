using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Students").HasKey(s => s.Id);

        builder.Property(s => s.Id).HasColumnName("Id").IsRequired();
        builder.Property(s => s.UserId).HasColumnName("UserId");
        builder.Property(s => s.CityId).HasColumnName("CityId");
        builder.Property(s => s.DistrictId).HasColumnName("DistrictId");
        builder.Property(s => s.NationalIdentity).HasColumnName("NationalIdentity");
        builder.Property(s => s.Phone).HasColumnName("Phone");
        builder.Property(s => s.BirthDate).HasColumnName("BirthDate");
        builder.Property(s => s.AddressDetail).HasColumnName("AddressDetail");
        builder.Property(s => s.Description).HasColumnName("Description");
        builder.Property(s => s.ProfilePhotoPath).HasColumnName("ProfilePhotoPath");
        builder.Property(s => s.Country).HasColumnName("Country");
        builder.Property(s => s.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(s => s.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(s => s.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(s => !s.DeletedDate.HasValue);
    }
}
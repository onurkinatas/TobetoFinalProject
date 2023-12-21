using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ClassExamConfiguration : IEntityTypeConfiguration<ClassExam>
{
    public void Configure(EntityTypeBuilder<ClassExam> builder)
    {
        builder.ToTable("ClassExams").HasKey(ce => ce.Id);

        builder.Property(ce => ce.Id).HasColumnName("Id").IsRequired();
        builder.Property(ce => ce.ExamId).HasColumnName("ExamId");
        builder.Property(ce => ce.StudentClassId).HasColumnName("StudentClassId");
        builder.Property(ce => ce.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(ce => ce.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(ce => ce.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(ce => !ce.DeletedDate.HasValue);
    }
}
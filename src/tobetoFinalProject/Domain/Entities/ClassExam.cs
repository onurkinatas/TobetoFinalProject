using Core.Persistence.Repositories;

namespace Domain.Entities;

public class ClassExam : Entity<Guid>
{
    public Guid ExamId { get; set; }
    public Guid StudentClassId { get; set; }
    public virtual Exam? Exam { get; set; }
    public virtual StudentClass StudentClass { get; set; }
}
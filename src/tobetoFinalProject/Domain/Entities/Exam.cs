using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Exam : Entity<Guid>
{
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public string ExamUrl { get; set; }
    public virtual ICollection<ClassExam>? ClassExams { get; set; }
}
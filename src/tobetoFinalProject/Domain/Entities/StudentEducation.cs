using Core.Persistence.Repositories;

namespace Domain.Entities;

public class StudentEducation : Entity<Guid>
{
    public Guid StudentId { get; set; }
    public virtual Student? Student { get; set; }
    public string EducationStatus { get; set; }
    public string SchoolName { get; set; }
    public string Branch { get; set; }
    public bool IsContinued { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime GraduationDate { get; set; }
}

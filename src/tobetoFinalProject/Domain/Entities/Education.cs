using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Education : Entity<Guid>
{
    public string EducationStatus { get; set; }
    public string SchoolName { get; set; }
    public string Branch { get; set; }
    public bool IsContinued { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime GraduationDate { get; set; }
    public virtual ICollection<StudentEducation>? StudentEducations { get; set; }
}



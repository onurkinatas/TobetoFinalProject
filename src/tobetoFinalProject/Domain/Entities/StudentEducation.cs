using Core.Persistence.Repositories;

namespace Domain.Entities;

public class StudentEducation : Entity<Guid>
{
    public Guid StudentId { get; set; }
    public Guid EducationId { get; set; }
    public virtual Student? Student { get; set; }
    public virtual Education? Education { get; set; }
}





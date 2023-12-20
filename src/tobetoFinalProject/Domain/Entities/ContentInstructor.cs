using Core.Persistence.Repositories;

namespace Domain.Entities;

public class ContentInstructor : Entity<Guid>
{
    public Guid ContentId { get; set; }
    public Guid InstructorId { get; set; }
    public virtual Instructor? Instructor { get; set; }
    public virtual Content? Content { get; set; }

}



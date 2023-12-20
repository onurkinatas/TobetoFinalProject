using Core.Persistence.Repositories;

namespace Domain.Entities;

public class StudentClassStudent : Entity<Guid>
{
    public Guid StudentId { get; set; }
    public Guid StudentClassId { get; set; }
    public virtual Student? Student { get; set; }
    public virtual StudentClass? StudentClass { get; set; }
}



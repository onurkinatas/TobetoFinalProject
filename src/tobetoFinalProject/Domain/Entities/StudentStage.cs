using Core.Persistence.Repositories;

namespace Domain.Entities;

public class StudentStage : Entity<Guid>
{
    public Guid StageId { get; set; }
    public Guid StudentId { get; set; }
    public virtual Student? Student { get; set; }
    public virtual Stage? Stage { get; set; }
}

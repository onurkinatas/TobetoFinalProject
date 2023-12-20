using Core.Persistence.Repositories;

namespace Domain.Entities;

public class StudentAppeal : Entity<Guid>
{
    public Guid StudentId { get; set; }
    public Guid AppealId { get; set; }
    public virtual Student? Student { get; set; }
    public virtual Appeal? Appeal { get; set; }
}

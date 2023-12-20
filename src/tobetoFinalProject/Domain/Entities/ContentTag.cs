using Core.Persistence.Repositories;

namespace Domain.Entities;

public class ContentTag : Entity<Guid>
{
    public Guid ContentId { get; set; }
    public Guid TagId { get; set; }
    public virtual Tag? Tag { get; set; }
    public virtual Content? Content { get; set; }

}

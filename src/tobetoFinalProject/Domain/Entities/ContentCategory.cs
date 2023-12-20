using Core.Persistence.Repositories;

namespace Domain.Entities;

public class ContentCategory : Entity<Guid>
{
    public string Name { get; set; }
    public virtual ICollection<Content>? Contents { get; set; }
}



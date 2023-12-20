using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Tag : Entity<Guid>
{
    public string Name { get; set; }
    public virtual ICollection<ContentTag>? ContentTags { get; set; }

}
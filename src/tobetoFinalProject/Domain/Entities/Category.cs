using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Category : Entity<Guid>
{
    public string Name { get; set; }
    public virtual ICollection<Lecture>? Lectures { get; set; }
}
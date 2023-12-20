using Core.Persistence.Repositories;

namespace Domain.Entities;

public class SubType : Entity<Guid>
{
    public string Name { get; set; }
}

using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Manufacturer : Entity<Guid>
{
    public string Name { get; set; }
}



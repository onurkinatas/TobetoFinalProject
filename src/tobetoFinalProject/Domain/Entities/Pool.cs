using Core.Persistence.Repositories;
using System.Security.Cryptography;

namespace Domain.Entities;

public class Pool:Entity<int>
{
    public string Name { get; set; }
    public virtual ICollection<PoolQuestion>? PoolQuestions { get; set; }
}



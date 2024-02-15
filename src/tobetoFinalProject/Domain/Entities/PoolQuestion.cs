using Core.Persistence.Repositories;

namespace Domain.Entities;

public class PoolQuestion : Entity<int>
{
    public int PoolId { get; set; }
    public int QuestionId { get; set; }
    public virtual Pool? Pool { get; set; }
    public virtual Question? Question { get; set; }
}



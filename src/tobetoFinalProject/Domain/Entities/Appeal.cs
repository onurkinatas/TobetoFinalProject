using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Appeal : Entity<Guid>
{
    public string Name { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public virtual ICollection<AppealStage>? AppealStages { get; set; }

}

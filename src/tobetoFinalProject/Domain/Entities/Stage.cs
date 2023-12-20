using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Stage : Entity<Guid>
{
    public string Description { get; set; }
    public virtual ICollection<StudentStage>? StudentStages { get; set; }
    public virtual ICollection<AppealStage>? AppealStages { get; set; }
}

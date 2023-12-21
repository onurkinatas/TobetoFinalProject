using Core.Persistence.Repositories;
namespace Domain.Entities;

public class AppealStage : Entity<Guid>
{
    public Guid AppealId { get; set; }
    public Guid StageId { get; set; }
    public virtual Appeal? Appeal { get; set; }
    public virtual Stage? Stage { get; set; }
}

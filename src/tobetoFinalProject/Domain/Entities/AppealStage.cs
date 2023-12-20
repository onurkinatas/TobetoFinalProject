using Core.Persistence.Repositories;

namespace Domain.Entities;

public class AppealStage : Entity<Guid>
{
    public Guid AppealId { get; set; }
    public Guid StageId { get; set; }
    public Appeal? Appeal { get; set; }
    public Stage? Stage { get; set; }
}

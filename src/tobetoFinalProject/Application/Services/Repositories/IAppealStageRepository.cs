using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IAppealStageRepository : IAsyncRepository<AppealStage, Guid>, IRepository<AppealStage, Guid>
{
}
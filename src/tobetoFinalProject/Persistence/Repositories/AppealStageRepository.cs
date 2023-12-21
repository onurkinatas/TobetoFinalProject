using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class AppealStageRepository : EfRepositoryBase<AppealStage, Guid, BaseDbContext>, IAppealStageRepository
{
    public AppealStageRepository(BaseDbContext context) : base(context)
    {
    }
}
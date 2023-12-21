using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class StageRepository : EfRepositoryBase<Stage, Guid, BaseDbContext>, IStageRepository
{
    public StageRepository(BaseDbContext context) : base(context)
    {
    }
}
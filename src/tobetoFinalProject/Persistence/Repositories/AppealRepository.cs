using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class AppealRepository : EfRepositoryBase<Appeal, Guid, BaseDbContext>, IAppealRepository
{
    public AppealRepository(BaseDbContext context) : base(context)
    {
    }
}
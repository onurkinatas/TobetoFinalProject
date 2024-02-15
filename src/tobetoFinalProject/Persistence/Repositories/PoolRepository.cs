using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;
using System.Linq.Expressions;

namespace Persistence.Repositories;

public class PoolRepository : EfRepositoryBase<Pool, int, BaseDbContext>, IPoolRepository
{
    public PoolRepository(BaseDbContext context) : base(context)
    {
    }
    public async Task<List<Pool>> GetAll(Expression<Func<Pool, bool>> filter = null)
    {
        return filter == null ? Context.Set<Pool>().ToList()
            : Context.Set<Pool>().Where(e => e.DeletedDate == null).Where(filter).ToList();
    }
}
using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;
using System.Linq.Expressions;

namespace Persistence.Repositories;

public class PoolQuestionRepository : EfRepositoryBase<PoolQuestion, int, BaseDbContext>, IPoolQuestionRepository
{
    public PoolQuestionRepository(BaseDbContext context) : base(context)
    {
    }
    public async Task<List<PoolQuestion>> GetAll(Expression<Func<PoolQuestion, bool>> filter = null)
    {
        return filter == null ? Context.Set<PoolQuestion>().ToList()
            : Context.Set<PoolQuestion>().Where(e => e.DeletedDate == null).Where(filter).ToList();
    }
}
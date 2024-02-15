using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Pools;

public interface IPoolsService
{
    Task<Pool?> GetAsync(
        Expression<Func<Pool, bool>> predicate,
        Func<IQueryable<Pool>, IIncludableQueryable<Pool, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Pool>?> GetListAsync(
        Expression<Func<Pool, bool>>? predicate = null,
        Func<IQueryable<Pool>, IOrderedQueryable<Pool>>? orderBy = null,
        Func<IQueryable<Pool>, IIncludableQueryable<Pool, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Pool> AddAsync(Pool pool);
    Task<Pool> UpdateAsync(Pool pool);
    Task<Pool> DeleteAsync(Pool pool, bool permanent = false);
}

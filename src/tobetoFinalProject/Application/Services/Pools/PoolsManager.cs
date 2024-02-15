using Application.Features.Pools.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Pools;

public class PoolsManager : IPoolsService
{
    private readonly IPoolRepository _poolRepository;
    private readonly PoolBusinessRules _poolBusinessRules;

    public PoolsManager(IPoolRepository poolRepository, PoolBusinessRules poolBusinessRules)
    {
        _poolRepository = poolRepository;
        _poolBusinessRules = poolBusinessRules;
    }

    public async Task<Pool?> GetAsync(
        Expression<Func<Pool, bool>> predicate,
        Func<IQueryable<Pool>, IIncludableQueryable<Pool, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Pool? pool = await _poolRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return pool;
    }

    public async Task<IPaginate<Pool>?> GetListAsync(
        Expression<Func<Pool, bool>>? predicate = null,
        Func<IQueryable<Pool>, IOrderedQueryable<Pool>>? orderBy = null,
        Func<IQueryable<Pool>, IIncludableQueryable<Pool, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Pool> poolList = await _poolRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return poolList;
    }

    public async Task<Pool> AddAsync(Pool pool)
    {
        Pool addedPool = await _poolRepository.AddAsync(pool);

        return addedPool;
    }

    public async Task<Pool> UpdateAsync(Pool pool)
    {
        Pool updatedPool = await _poolRepository.UpdateAsync(pool);

        return updatedPool;
    }

    public async Task<Pool> DeleteAsync(Pool pool, bool permanent = false)
    {
        Pool deletedPool = await _poolRepository.DeleteAsync(pool);

        return deletedPool;
    }

   
}

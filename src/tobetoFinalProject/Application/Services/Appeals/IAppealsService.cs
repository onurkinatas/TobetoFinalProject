using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Appeals;

public interface IAppealsService
{
    Task<Appeal?> GetAsync(
        Expression<Func<Appeal, bool>> predicate,
        Func<IQueryable<Appeal>, IIncludableQueryable<Appeal, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Appeal>?> GetListAsync(
        Expression<Func<Appeal, bool>>? predicate = null,
        Func<IQueryable<Appeal>, IOrderedQueryable<Appeal>>? orderBy = null,
        Func<IQueryable<Appeal>, IIncludableQueryable<Appeal, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Appeal> AddAsync(Appeal appeal);
    Task<Appeal> UpdateAsync(Appeal appeal);
    Task<Appeal> DeleteAsync(Appeal appeal, bool permanent = false);
}

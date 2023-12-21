using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.SubTypes;

public interface ISubTypesService
{
    Task<SubType?> GetAsync(
        Expression<Func<SubType, bool>> predicate,
        Func<IQueryable<SubType>, IIncludableQueryable<SubType, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<SubType>?> GetListAsync(
        Expression<Func<SubType, bool>>? predicate = null,
        Func<IQueryable<SubType>, IOrderedQueryable<SubType>>? orderBy = null,
        Func<IQueryable<SubType>, IIncludableQueryable<SubType, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<SubType> AddAsync(SubType subType);
    Task<SubType> UpdateAsync(SubType subType);
    Task<SubType> DeleteAsync(SubType subType, bool permanent = false);
}

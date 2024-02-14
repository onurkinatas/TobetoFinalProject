using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Options;

public interface IOptionsService
{
    Task<Option?> GetAsync(
        Expression<Func<Option, bool>> predicate,
        Func<IQueryable<Option>, IIncludableQueryable<Option, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Option>?> GetListAsync(
        Expression<Func<Option, bool>>? predicate = null,
        Func<IQueryable<Option>, IOrderedQueryable<Option>>? orderBy = null,
        Func<IQueryable<Option>, IIncludableQueryable<Option, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Option> AddAsync(Option option);
    Task<Option> UpdateAsync(Option option);
    Task<Option> DeleteAsync(Option option, bool permanent = false);
}

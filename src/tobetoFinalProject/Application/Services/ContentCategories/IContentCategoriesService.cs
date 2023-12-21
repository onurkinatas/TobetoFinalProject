using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ContentCategories;

public interface IContentCategoriesService
{
    Task<ContentCategory?> GetAsync(
        Expression<Func<ContentCategory, bool>> predicate,
        Func<IQueryable<ContentCategory>, IIncludableQueryable<ContentCategory, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<ContentCategory>?> GetListAsync(
        Expression<Func<ContentCategory, bool>>? predicate = null,
        Func<IQueryable<ContentCategory>, IOrderedQueryable<ContentCategory>>? orderBy = null,
        Func<IQueryable<ContentCategory>, IIncludableQueryable<ContentCategory, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<ContentCategory> AddAsync(ContentCategory contentCategory);
    Task<ContentCategory> UpdateAsync(ContentCategory contentCategory);
    Task<ContentCategory> DeleteAsync(ContentCategory contentCategory, bool permanent = false);
}

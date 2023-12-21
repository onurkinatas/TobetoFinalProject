using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ContentTags;

public interface IContentTagsService
{
    Task<ContentTag?> GetAsync(
        Expression<Func<ContentTag, bool>> predicate,
        Func<IQueryable<ContentTag>, IIncludableQueryable<ContentTag, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<ContentTag>?> GetListAsync(
        Expression<Func<ContentTag, bool>>? predicate = null,
        Func<IQueryable<ContentTag>, IOrderedQueryable<ContentTag>>? orderBy = null,
        Func<IQueryable<ContentTag>, IIncludableQueryable<ContentTag, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<ContentTag> AddAsync(ContentTag contentTag);
    Task<ContentTag> UpdateAsync(ContentTag contentTag);
    Task<ContentTag> DeleteAsync(ContentTag contentTag, bool permanent = false);
}

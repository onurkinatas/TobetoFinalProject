using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ContentLikes;

public interface IContentLikesService
{
    Task<ContentLike?> GetAsync(
        Expression<Func<ContentLike, bool>> predicate,
        Func<IQueryable<ContentLike>, IIncludableQueryable<ContentLike, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<ContentLike>?> GetListAsync(
        Expression<Func<ContentLike, bool>>? predicate = null,
        Func<IQueryable<ContentLike>, IOrderedQueryable<ContentLike>>? orderBy = null,
        Func<IQueryable<ContentLike>, IIncludableQueryable<ContentLike, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<ContentLike> AddAsync(ContentLike contentLike);
    Task<ContentLike> UpdateAsync(ContentLike contentLike);
    Task<ContentLike> DeleteAsync(ContentLike contentLike, bool permanent = false);
    Task<int> GetContentLikeCount(Guid contentId);
}

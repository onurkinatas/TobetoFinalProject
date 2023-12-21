using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Contents;

public interface IContentsService
{
    Task<Content?> GetAsync(
        Expression<Func<Content, bool>> predicate,
        Func<IQueryable<Content>, IIncludableQueryable<Content, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Content>?> GetListAsync(
        Expression<Func<Content, bool>>? predicate = null,
        Func<IQueryable<Content>, IOrderedQueryable<Content>>? orderBy = null,
        Func<IQueryable<Content>, IIncludableQueryable<Content, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Content> AddAsync(Content content);
    Task<Content> UpdateAsync(Content content);
    Task<Content> DeleteAsync(Content content, bool permanent = false);
}

using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Tags;

public interface ITagsService
{
    Task<Tag?> GetAsync(
        Expression<Func<Tag, bool>> predicate,
        Func<IQueryable<Tag>, IIncludableQueryable<Tag, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Tag>?> GetListAsync(
        Expression<Func<Tag, bool>>? predicate = null,
        Func<IQueryable<Tag>, IOrderedQueryable<Tag>>? orderBy = null,
        Func<IQueryable<Tag>, IIncludableQueryable<Tag, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Tag> AddAsync(Tag tag);
    Task<Tag> UpdateAsync(Tag tag);
    Task<Tag> DeleteAsync(Tag tag, bool permanent = false);
}

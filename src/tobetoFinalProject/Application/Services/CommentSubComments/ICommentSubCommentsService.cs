using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.CommentSubComments;

public interface ICommentSubCommentsService
{
    Task<CommentSubComment?> GetAsync(
        Expression<Func<CommentSubComment, bool>> predicate,
        Func<IQueryable<CommentSubComment>, IIncludableQueryable<CommentSubComment, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<CommentSubComment>?> GetListAsync(
        Expression<Func<CommentSubComment, bool>>? predicate = null,
        Func<IQueryable<CommentSubComment>, IOrderedQueryable<CommentSubComment>>? orderBy = null,
        Func<IQueryable<CommentSubComment>, IIncludableQueryable<CommentSubComment, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<CommentSubComment> AddAsync(CommentSubComment commentSubComment);
    Task<CommentSubComment> UpdateAsync(CommentSubComment commentSubComment);
    Task<CommentSubComment> DeleteAsync(CommentSubComment commentSubComment, bool permanent = false);
}

using Application.Features.ContentLikes.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ContentLikes;

public class ContentLikesManager : IContentLikesService
{
    private readonly IContentLikeRepository _contentLikeRepository;
    private readonly ContentLikeBusinessRules _contentLikeBusinessRules;

    public ContentLikesManager(IContentLikeRepository contentLikeRepository, ContentLikeBusinessRules contentLikeBusinessRules)
    {
        _contentLikeRepository = contentLikeRepository;
        _contentLikeBusinessRules = contentLikeBusinessRules;
    }

    public async Task<ContentLike?> GetAsync(
        Expression<Func<ContentLike, bool>> predicate,
        Func<IQueryable<ContentLike>, IIncludableQueryable<ContentLike, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        ContentLike? contentLike = await _contentLikeRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return contentLike;
    }

    public async Task<IPaginate<ContentLike>?> GetListAsync(
        Expression<Func<ContentLike, bool>>? predicate = null,
        Func<IQueryable<ContentLike>, IOrderedQueryable<ContentLike>>? orderBy = null,
        Func<IQueryable<ContentLike>, IIncludableQueryable<ContentLike, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<ContentLike> contentLikeList = await _contentLikeRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return contentLikeList;
    }

    public async Task<ContentLike> AddAsync(ContentLike contentLike)
    {
        ContentLike addedContentLike = await _contentLikeRepository.AddAsync(contentLike);

        return addedContentLike;
    }

    public async Task<ContentLike> UpdateAsync(ContentLike contentLike)
    {
        ContentLike updatedContentLike = await _contentLikeRepository.UpdateAsync(contentLike);

        return updatedContentLike;
    }

    public async Task<ContentLike> DeleteAsync(ContentLike contentLike, bool permanent = false)
    {
        ContentLike deletedContentLike = await _contentLikeRepository.DeleteAsync(contentLike);

        return deletedContentLike;
    }
}

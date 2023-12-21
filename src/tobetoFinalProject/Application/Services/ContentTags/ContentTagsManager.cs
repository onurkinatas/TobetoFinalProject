using Application.Features.ContentTags.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ContentTags;

public class ContentTagsManager : IContentTagsService
{
    private readonly IContentTagRepository _contentTagRepository;
    private readonly ContentTagBusinessRules _contentTagBusinessRules;

    public ContentTagsManager(IContentTagRepository contentTagRepository, ContentTagBusinessRules contentTagBusinessRules)
    {
        _contentTagRepository = contentTagRepository;
        _contentTagBusinessRules = contentTagBusinessRules;
    }

    public async Task<ContentTag?> GetAsync(
        Expression<Func<ContentTag, bool>> predicate,
        Func<IQueryable<ContentTag>, IIncludableQueryable<ContentTag, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        ContentTag? contentTag = await _contentTagRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return contentTag;
    }

    public async Task<IPaginate<ContentTag>?> GetListAsync(
        Expression<Func<ContentTag, bool>>? predicate = null,
        Func<IQueryable<ContentTag>, IOrderedQueryable<ContentTag>>? orderBy = null,
        Func<IQueryable<ContentTag>, IIncludableQueryable<ContentTag, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<ContentTag> contentTagList = await _contentTagRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return contentTagList;
    }

    public async Task<ContentTag> AddAsync(ContentTag contentTag)
    {
        ContentTag addedContentTag = await _contentTagRepository.AddAsync(contentTag);

        return addedContentTag;
    }

    public async Task<ContentTag> UpdateAsync(ContentTag contentTag)
    {
        ContentTag updatedContentTag = await _contentTagRepository.UpdateAsync(contentTag);

        return updatedContentTag;
    }

    public async Task<ContentTag> DeleteAsync(ContentTag contentTag, bool permanent = false)
    {
        ContentTag deletedContentTag = await _contentTagRepository.DeleteAsync(contentTag);

        return deletedContentTag;
    }
}

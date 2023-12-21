using Application.Features.Tags.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Tags;

public class TagsManager : ITagsService
{
    private readonly ITagRepository _tagRepository;
    private readonly TagBusinessRules _tagBusinessRules;

    public TagsManager(ITagRepository tagRepository, TagBusinessRules tagBusinessRules)
    {
        _tagRepository = tagRepository;
        _tagBusinessRules = tagBusinessRules;
    }

    public async Task<Tag?> GetAsync(
        Expression<Func<Tag, bool>> predicate,
        Func<IQueryable<Tag>, IIncludableQueryable<Tag, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Tag? tag = await _tagRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return tag;
    }

    public async Task<IPaginate<Tag>?> GetListAsync(
        Expression<Func<Tag, bool>>? predicate = null,
        Func<IQueryable<Tag>, IOrderedQueryable<Tag>>? orderBy = null,
        Func<IQueryable<Tag>, IIncludableQueryable<Tag, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Tag> tagList = await _tagRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return tagList;
    }

    public async Task<Tag> AddAsync(Tag tag)
    {
        Tag addedTag = await _tagRepository.AddAsync(tag);

        return addedTag;
    }

    public async Task<Tag> UpdateAsync(Tag tag)
    {
        Tag updatedTag = await _tagRepository.UpdateAsync(tag);

        return updatedTag;
    }

    public async Task<Tag> DeleteAsync(Tag tag, bool permanent = false)
    {
        Tag deletedTag = await _tagRepository.DeleteAsync(tag);

        return deletedTag;
    }
}

using Application.Features.ContentTags.Constants;
using Application.Features.ContentTags.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.ContentTags.Rules;

public class ContentTagBusinessRules : BaseBusinessRules
{
    private readonly IContentTagRepository _contentTagRepository;

    public ContentTagBusinessRules(IContentTagRepository contentTagRepository)
    {
        _contentTagRepository = contentTagRepository;
    }
    public async Task ContentTagShouldNotExistsWhenInsert(Guid contentId, Guid tagId)
    {
        bool doesExists = await _contentTagRepository
            .AnyAsync(predicate: ca => ca.TagId == tagId && ca.ContentId == contentId, enableTracking: false);
        if (doesExists)
            throw new BusinessException(ContentTagsBusinessMessages.ContentTagAlreadyExists);
    }
    public async Task ContentTagShouldNotExistsWhenUpdate(Guid contentId, Guid tagId)
    {
        bool doesExists = await _contentTagRepository
            .AnyAsync(predicate: ca => ca.TagId == tagId && ca.ContentId == contentId, enableTracking: false);
        if (doesExists)
            throw new BusinessException(ContentTagsBusinessMessages.ContentTagAlreadyExists);
    }
    public Task ContentTagShouldExistWhenSelected(ContentTag? contentTag)
    {
        if (contentTag == null)
            throw new BusinessException(ContentTagsBusinessMessages.ContentTagNotExists);
        return Task.CompletedTask;
    }

    public async Task ContentTagIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        ContentTag? contentTag = await _contentTagRepository.GetAsync(
            predicate: ct => ct.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ContentTagShouldExistWhenSelected(contentTag);
    }
}
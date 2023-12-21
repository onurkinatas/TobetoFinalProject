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
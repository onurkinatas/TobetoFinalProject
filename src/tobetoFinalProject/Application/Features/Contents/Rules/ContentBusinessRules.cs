using Application.Features.Contents.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Contents.Rules;

public class ContentBusinessRules : BaseBusinessRules
{
    private readonly IContentRepository _contentRepository;

    public ContentBusinessRules(IContentRepository contentRepository)
    {
        _contentRepository = contentRepository;
    }

    public Task ContentShouldExistWhenSelected(Content? content)
    {
        if (content == null)
            throw new BusinessException(ContentsBusinessMessages.ContentNotExists);
        return Task.CompletedTask;
    }

    public async Task ContentIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Content? content = await _contentRepository.GetAsync(
            predicate: c => c.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ContentShouldExistWhenSelected(content);
    }
}
using Application.Features.Contents.Constants;
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

    public async Task ContentShouldNotExistsWhenInsert(string name)
    {
        bool doesExists = await _contentRepository
            .AnyAsync(predicate: ca => ca.Name == name, enableTracking: false);
        if (doesExists)
            throw new BusinessException(ContentsBusinessMessages.ContentNameExists);
    }
    public async Task ContentShouldNotExistsWhenUpdate(string name)
    {
        bool doesExists = await _contentRepository
            .AnyAsync(predicate: ca => ca.Name == name, enableTracking: false);
        if (doesExists)
            throw new BusinessException(ContentsBusinessMessages.ContentNameExists);
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

using Application.Features.Cities.Constants;
using Application.Features.ContentCategories.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.ContentCategories.Rules;

public class ContentCategoryBusinessRules : BaseBusinessRules
{
    private readonly IContentCategoryRepository _contentCategoryRepository;

    public ContentCategoryBusinessRules(IContentCategoryRepository contentCategoryRepository)
    {
        _contentCategoryRepository = contentCategoryRepository;
    }
    public async Task ContentCategoryShouldNotExistsWhenInsert(string name)
    {
        bool doesExists = await _contentCategoryRepository
            .AnyAsync(predicate: ca => ca.Name == name, enableTracking: false);
        if (doesExists)
            throw new BusinessException(ContentCategoriesBusinessMessages.ContentCategoryNameExists);
    }
    public async Task ContentCategoryShouldNotExistsWhenUpdate(string name)
    {
        bool doesExists = await _contentCategoryRepository
            .AnyAsync(predicate: ca => ca.Name == name, enableTracking: false);
        if (doesExists)
            throw new BusinessException(ContentCategoriesBusinessMessages.ContentCategoryNameExists);
    }
    public Task ContentCategoryShouldExistWhenSelected(ContentCategory? contentCategory)
    {
        if (contentCategory == null)
            throw new BusinessException(ContentCategoriesBusinessMessages.ContentCategoryNotExists);
        return Task.CompletedTask;
    }

    public async Task ContentCategoryIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        ContentCategory? contentCategory = await _contentCategoryRepository.GetAsync(
            predicate: cc => cc.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ContentCategoryShouldExistWhenSelected(contentCategory);
    }
}
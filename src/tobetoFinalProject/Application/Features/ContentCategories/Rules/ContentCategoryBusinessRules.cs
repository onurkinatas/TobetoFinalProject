using Application.Features.ContentCategorys.Constants;
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
    public Task ContentCategoryShouldNotExist(ContentCategory? contentCategory)
    {
        if (contentCategory != null)
            throw new BusinessException(ContentCategoriesBusinessMessages.ContentCategoryNameExists);
        return Task.CompletedTask;
    }
    public async Task ContentCategoryNameShouldNotExist(ContentCategory contentCategory, CancellationToken cancellationToken)
    {
        ContentCategory? controlContentCategory = await _contentCategoryRepository.GetAsync(
            predicate: a => a.Name == contentCategory.Name,
            enableTracking: false,
            cancellationToken: cancellationToken
            );
        await ContentCategoryShouldNotExist(controlContentCategory);
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
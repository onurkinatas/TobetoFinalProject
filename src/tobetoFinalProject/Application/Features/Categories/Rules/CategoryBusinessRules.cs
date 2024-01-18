using Application.Features.Appeals.Constants;
using Application.Features.Categories.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Categories.Rules;

public class CategoryBusinessRules : BaseBusinessRules
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryBusinessRules(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public Task CategoryShouldExistWhenSelected(Category? category)
    {
        if (category == null)
            throw new BusinessException(CategoriesBusinessMessages.CategoryNotExists);
        return Task.CompletedTask;
    }

    public async Task CategoryIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Category? category = await _categoryRepository.GetAsync(
            predicate: c => c.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await CategoryShouldExistWhenSelected(category);
    }

    public Task CategoryShouldNotExist(Category? category)
    {
        if (category != null)
            throw new BusinessException(CategoriesBusinessMessages.CategoryNameExists);
        return Task.CompletedTask;
    }
    public async Task CategoryNameShouldNotExist(Category category, CancellationToken cancellationToken)
    {
        Category? controlCategory = await _categoryRepository.GetAsync(
            predicate: a => a.Name == category.Name,
            enableTracking: false,
            cancellationToken: cancellationToken
            );
        await CategoryShouldNotExist(controlCategory);
    }
}
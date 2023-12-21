using Application.Features.ContentCategories.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ContentCategories;

public class ContentCategoriesManager : IContentCategoriesService
{
    private readonly IContentCategoryRepository _contentCategoryRepository;
    private readonly ContentCategoryBusinessRules _contentCategoryBusinessRules;

    public ContentCategoriesManager(IContentCategoryRepository contentCategoryRepository, ContentCategoryBusinessRules contentCategoryBusinessRules)
    {
        _contentCategoryRepository = contentCategoryRepository;
        _contentCategoryBusinessRules = contentCategoryBusinessRules;
    }

    public async Task<ContentCategory?> GetAsync(
        Expression<Func<ContentCategory, bool>> predicate,
        Func<IQueryable<ContentCategory>, IIncludableQueryable<ContentCategory, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        ContentCategory? contentCategory = await _contentCategoryRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return contentCategory;
    }

    public async Task<IPaginate<ContentCategory>?> GetListAsync(
        Expression<Func<ContentCategory, bool>>? predicate = null,
        Func<IQueryable<ContentCategory>, IOrderedQueryable<ContentCategory>>? orderBy = null,
        Func<IQueryable<ContentCategory>, IIncludableQueryable<ContentCategory, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<ContentCategory> contentCategoryList = await _contentCategoryRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return contentCategoryList;
    }

    public async Task<ContentCategory> AddAsync(ContentCategory contentCategory)
    {
        ContentCategory addedContentCategory = await _contentCategoryRepository.AddAsync(contentCategory);

        return addedContentCategory;
    }

    public async Task<ContentCategory> UpdateAsync(ContentCategory contentCategory)
    {
        ContentCategory updatedContentCategory = await _contentCategoryRepository.UpdateAsync(contentCategory);

        return updatedContentCategory;
    }

    public async Task<ContentCategory> DeleteAsync(ContentCategory contentCategory, bool permanent = false)
    {
        ContentCategory deletedContentCategory = await _contentCategoryRepository.DeleteAsync(contentCategory);

        return deletedContentCategory;
    }
}

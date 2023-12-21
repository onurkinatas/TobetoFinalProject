using Application.Features.Contents.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Contents;

public class ContentsManager : IContentsService
{
    private readonly IContentRepository _contentRepository;
    private readonly ContentBusinessRules _contentBusinessRules;

    public ContentsManager(IContentRepository contentRepository, ContentBusinessRules contentBusinessRules)
    {
        _contentRepository = contentRepository;
        _contentBusinessRules = contentBusinessRules;
    }

    public async Task<Content?> GetAsync(
        Expression<Func<Content, bool>> predicate,
        Func<IQueryable<Content>, IIncludableQueryable<Content, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Content? content = await _contentRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return content;
    }

    public async Task<IPaginate<Content>?> GetListAsync(
        Expression<Func<Content, bool>>? predicate = null,
        Func<IQueryable<Content>, IOrderedQueryable<Content>>? orderBy = null,
        Func<IQueryable<Content>, IIncludableQueryable<Content, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Content> contentList = await _contentRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return contentList;
    }

    public async Task<Content> AddAsync(Content content)
    {
        Content addedContent = await _contentRepository.AddAsync(content);

        return addedContent;
    }

    public async Task<Content> UpdateAsync(Content content)
    {
        Content updatedContent = await _contentRepository.UpdateAsync(content);

        return updatedContent;
    }

    public async Task<Content> DeleteAsync(Content content, bool permanent = false)
    {
        Content deletedContent = await _contentRepository.DeleteAsync(content);

        return deletedContent;
    }
}

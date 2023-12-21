using Application.Features.ContentInstructors.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ContentInstructors;

public class ContentInstructorsManager : IContentInstructorsService
{
    private readonly IContentInstructorRepository _contentInstructorRepository;
    private readonly ContentInstructorBusinessRules _contentInstructorBusinessRules;

    public ContentInstructorsManager(IContentInstructorRepository contentInstructorRepository, ContentInstructorBusinessRules contentInstructorBusinessRules)
    {
        _contentInstructorRepository = contentInstructorRepository;
        _contentInstructorBusinessRules = contentInstructorBusinessRules;
    }

    public async Task<ContentInstructor?> GetAsync(
        Expression<Func<ContentInstructor, bool>> predicate,
        Func<IQueryable<ContentInstructor>, IIncludableQueryable<ContentInstructor, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        ContentInstructor? contentInstructor = await _contentInstructorRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return contentInstructor;
    }

    public async Task<IPaginate<ContentInstructor>?> GetListAsync(
        Expression<Func<ContentInstructor, bool>>? predicate = null,
        Func<IQueryable<ContentInstructor>, IOrderedQueryable<ContentInstructor>>? orderBy = null,
        Func<IQueryable<ContentInstructor>, IIncludableQueryable<ContentInstructor, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<ContentInstructor> contentInstructorList = await _contentInstructorRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return contentInstructorList;
    }

    public async Task<ContentInstructor> AddAsync(ContentInstructor contentInstructor)
    {
        ContentInstructor addedContentInstructor = await _contentInstructorRepository.AddAsync(contentInstructor);

        return addedContentInstructor;
    }

    public async Task<ContentInstructor> UpdateAsync(ContentInstructor contentInstructor)
    {
        ContentInstructor updatedContentInstructor = await _contentInstructorRepository.UpdateAsync(contentInstructor);

        return updatedContentInstructor;
    }

    public async Task<ContentInstructor> DeleteAsync(ContentInstructor contentInstructor, bool permanent = false)
    {
        ContentInstructor deletedContentInstructor = await _contentInstructorRepository.DeleteAsync(contentInstructor);

        return deletedContentInstructor;
    }
}

using Application.Features.CourseContents.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.CourseContents;

public class CourseContentsManager : ICourseContentsService
{
    private readonly ICourseContentRepository _courseContentRepository;
    private readonly CourseContentBusinessRules _courseContentBusinessRules;

    public CourseContentsManager(ICourseContentRepository courseContentRepository, CourseContentBusinessRules courseContentBusinessRules)
    {
        _courseContentRepository = courseContentRepository;
        _courseContentBusinessRules = courseContentBusinessRules;
    }

    public async Task<CourseContent?> GetAsync(
        Expression<Func<CourseContent, bool>> predicate,
        Func<IQueryable<CourseContent>, IIncludableQueryable<CourseContent, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        CourseContent? courseContent = await _courseContentRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return courseContent;
    }

    public async Task<IPaginate<CourseContent>?> GetListAsync(
        Expression<Func<CourseContent, bool>>? predicate = null,
        Func<IQueryable<CourseContent>, IOrderedQueryable<CourseContent>>? orderBy = null,
        Func<IQueryable<CourseContent>, IIncludableQueryable<CourseContent, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<CourseContent> courseContentList = await _courseContentRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return courseContentList;
    }

    public async Task<CourseContent> AddAsync(CourseContent courseContent)
    {
        CourseContent addedCourseContent = await _courseContentRepository.AddAsync(courseContent);

        return addedCourseContent;
    }

    public async Task<CourseContent> UpdateAsync(CourseContent courseContent)
    {
        CourseContent updatedCourseContent = await _courseContentRepository.UpdateAsync(courseContent);

        return updatedCourseContent;
    }

    public async Task<CourseContent> DeleteAsync(CourseContent courseContent, bool permanent = false)
    {
        CourseContent deletedCourseContent = await _courseContentRepository.DeleteAsync(courseContent);

        return deletedCourseContent;
    }
}

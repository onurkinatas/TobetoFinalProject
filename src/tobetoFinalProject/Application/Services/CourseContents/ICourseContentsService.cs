using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.CourseContents;

public interface ICourseContentsService
{
    Task<CourseContent?> GetAsync(
        Expression<Func<CourseContent, bool>> predicate,
        Func<IQueryable<CourseContent>, IIncludableQueryable<CourseContent, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<CourseContent>?> GetListAsync(
        Expression<Func<CourseContent, bool>>? predicate = null,
        Func<IQueryable<CourseContent>, IOrderedQueryable<CourseContent>>? orderBy = null,
        Func<IQueryable<CourseContent>, IIncludableQueryable<CourseContent, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<CourseContent> AddAsync(CourseContent courseContent);
    Task<CourseContent> UpdateAsync(CourseContent courseContent);
    Task<CourseContent> DeleteAsync(CourseContent courseContent, bool permanent = false);
}

using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.LectureCourses;

public interface ILectureCoursesService
{
    Task<LectureCourse?> GetAsync(
        Expression<Func<LectureCourse, bool>> predicate,
        Func<IQueryable<LectureCourse>, IIncludableQueryable<LectureCourse, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<LectureCourse>?> GetListAsync(
        Expression<Func<LectureCourse, bool>>? predicate = null,
        Func<IQueryable<LectureCourse>, IOrderedQueryable<LectureCourse>>? orderBy = null,
        Func<IQueryable<LectureCourse>, IIncludableQueryable<LectureCourse, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<LectureCourse> AddAsync(LectureCourse lectureCourse);
    Task<LectureCourse> UpdateAsync(LectureCourse lectureCourse);
    Task<LectureCourse> DeleteAsync(LectureCourse lectureCourse, bool permanent = false);
}

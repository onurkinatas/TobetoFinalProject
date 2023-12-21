using Application.Features.LectureCourses.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.LectureCourses;

public class LectureCoursesManager : ILectureCoursesService
{
    private readonly ILectureCourseRepository _lectureCourseRepository;
    private readonly LectureCourseBusinessRules _lectureCourseBusinessRules;

    public LectureCoursesManager(ILectureCourseRepository lectureCourseRepository, LectureCourseBusinessRules lectureCourseBusinessRules)
    {
        _lectureCourseRepository = lectureCourseRepository;
        _lectureCourseBusinessRules = lectureCourseBusinessRules;
    }

    public async Task<LectureCourse?> GetAsync(
        Expression<Func<LectureCourse, bool>> predicate,
        Func<IQueryable<LectureCourse>, IIncludableQueryable<LectureCourse, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        LectureCourse? lectureCourse = await _lectureCourseRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return lectureCourse;
    }

    public async Task<IPaginate<LectureCourse>?> GetListAsync(
        Expression<Func<LectureCourse, bool>>? predicate = null,
        Func<IQueryable<LectureCourse>, IOrderedQueryable<LectureCourse>>? orderBy = null,
        Func<IQueryable<LectureCourse>, IIncludableQueryable<LectureCourse, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<LectureCourse> lectureCourseList = await _lectureCourseRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return lectureCourseList;
    }

    public async Task<LectureCourse> AddAsync(LectureCourse lectureCourse)
    {
        LectureCourse addedLectureCourse = await _lectureCourseRepository.AddAsync(lectureCourse);

        return addedLectureCourse;
    }

    public async Task<LectureCourse> UpdateAsync(LectureCourse lectureCourse)
    {
        LectureCourse updatedLectureCourse = await _lectureCourseRepository.UpdateAsync(lectureCourse);

        return updatedLectureCourse;
    }

    public async Task<LectureCourse> DeleteAsync(LectureCourse lectureCourse, bool permanent = false)
    {
        LectureCourse deletedLectureCourse = await _lectureCourseRepository.DeleteAsync(lectureCourse);

        return deletedLectureCourse;
    }
}

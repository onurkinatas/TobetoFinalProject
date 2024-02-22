using Application.Features.Lectures.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using System.Threading;

namespace Application.Services.Lectures;

public class LecturesManager : ILecturesService
{
    private readonly ILectureRepository _lectureRepository;
    private readonly LectureBusinessRules _lectureBusinessRules;

    public LecturesManager(ILectureRepository lectureRepository, LectureBusinessRules lectureBusinessRules)
    {
        _lectureRepository = lectureRepository;
        _lectureBusinessRules = lectureBusinessRules;
    }

    public async Task<Lecture?> GetAsync(
        Expression<Func<Lecture, bool>> predicate,
        Func<IQueryable<Lecture>, IIncludableQueryable<Lecture, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Lecture? lecture = await _lectureRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return lecture;
    }

    public async Task<IPaginate<Lecture>?> GetListAsync(
        Expression<Func<Lecture, bool>>? predicate = null,
        Func<IQueryable<Lecture>, IOrderedQueryable<Lecture>>? orderBy = null,
        Func<IQueryable<Lecture>, IIncludableQueryable<Lecture, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Lecture> lectureList = await _lectureRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return lectureList;
    }

    public async Task<Lecture> AddAsync(Lecture lecture)
    {
        Lecture addedLecture = await _lectureRepository.AddAsync(lecture);

        return addedLecture;
    }

    public async Task<Lecture> UpdateAsync(Lecture lecture)
    {
        Lecture updatedLecture = await _lectureRepository.UpdateAsync(lecture);

        return updatedLecture;
    }

    public async Task<Lecture> DeleteAsync(Lecture lecture, bool permanent = false)
    {
        Lecture deletedLecture = await _lectureRepository.DeleteAsync(lecture);

        return deletedLecture;
    }

    public int GetAllContentCountByLecture(Lecture lecture,CancellationToken cancellationToken)
    {
        
        int contentCount = 
                lecture.LectureCourses.Count!=0 ? (lecture.LectureCourses.Select(lc => lc.Course.CourseContents).Count() != 0 ? lecture.LectureCourses
                    .SelectMany(lc => lc.Course.CourseContents)
                    .Count() : 0):0;
        return contentCount;
    }
    public async Task<int> GetAllContentCountByLectureId(Guid lectureId, CancellationToken cancellationToken)
    {
        Lecture lecture = await _lectureRepository.GetAsync(
                predicate: l => l.Id == lectureId,
                include: l => l.Include(l => l.LectureCourses)
                   .ThenInclude(lc => lc.Course)
                   .ThenInclude(c => c.CourseContents)
                   .ThenInclude(cc => cc.Content),
                cancellationToken: cancellationToken);

        int contentCount =
                lecture.LectureCourses.Count != 0 ? (lecture.LectureCourses.Select(lc => lc.Course.CourseContents).Count() != 0 ? lecture.LectureCourses
                    .SelectMany(lc => lc.Course.CourseContents)
                    .Count() : 0) : 0;

        return contentCount;
    }
}

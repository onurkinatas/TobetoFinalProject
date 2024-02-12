using Application.Features.ClassLectures.Rules;
using Application.Services.ContextOperations;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ClassLectures;

public class ClassLecturesManager : IClassLecturesService
{
    private readonly IClassLectureRepository _classLectureRepository;
    private readonly ClassLectureBusinessRules _classLectureBusinessRules;
    private readonly IContextOperationService _contextOperationService;

    public ClassLecturesManager(IClassLectureRepository classLectureRepository, ClassLectureBusinessRules classLectureBusinessRules, IContextOperationService contextOperationService)
    {
        _classLectureRepository = classLectureRepository;
        _classLectureBusinessRules = classLectureBusinessRules;
        _contextOperationService = contextOperationService;
    }

    public async Task<ClassLecture?> GetAsync(
        Expression<Func<ClassLecture, bool>> predicate,
        Func<IQueryable<ClassLecture>, IIncludableQueryable<ClassLecture, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        ClassLecture? classLecture = await _classLectureRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return classLecture;
    }

    public async Task<IPaginate<ClassLecture>?> GetListAsync(
        Expression<Func<ClassLecture, bool>>? predicate = null,
        Func<IQueryable<ClassLecture>, IOrderedQueryable<ClassLecture>>? orderBy = null,
        Func<IQueryable<ClassLecture>, IIncludableQueryable<ClassLecture, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<ClassLecture> classLectureList = await _classLectureRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return classLectureList;
    }

    public async Task<ClassLecture> AddAsync(ClassLecture classLecture)
    {
        ClassLecture addedClassLecture = await _classLectureRepository.AddAsync(classLecture);

        return addedClassLecture;
    }

    public async Task<ClassLecture> UpdateAsync(ClassLecture classLecture)
    {
        ClassLecture updatedClassLecture = await _classLectureRepository.UpdateAsync(classLecture);

        return updatedClassLecture;
    }

    public async Task<ClassLecture> DeleteAsync(ClassLecture classLecture, bool permanent = false)
    {
        ClassLecture deletedClassLecture = await _classLectureRepository.DeleteAsync(classLecture);

        return deletedClassLecture;
    }

    public async Task<int> GetAllContentCountForActiveStudent()
    {
        ICollection<Guid> activeStudentClasses = await _contextOperationService.GetStudentClassesFromContext();

        ClassLecture lecture = await _classLectureRepository.GetAsync(
                predicate: l => activeStudentClasses.Contains(l.StudentClassId),
                include: l => l.Include(l=>l.Lecture)
                    .ThenInclude(l => l.LectureCourses)
                   .ThenInclude(lc => lc.Course)
                   .ThenInclude(c => c.CourseContents)
                   .ThenInclude(cc => cc.Content));

        var contentCount = lecture.Lecture
            .LectureCourses
            .SelectMany(lc => lc.Course.CourseContents)
            .Count();

        return contentCount;
    }
}


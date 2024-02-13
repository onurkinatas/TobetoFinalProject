using Application.Features.StudentClasses.Rules;
using Application.Services.ContextOperations;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.StudentClasses;

public class StudentClassesManager : IStudentClassesService
{
    private readonly IStudentClassRepository _studentClassRepository;
    private readonly StudentClassBusinessRules _studentClassBusinessRules;
    private readonly IContextOperationService _contextOperationService;

    public StudentClassesManager(IStudentClassRepository studentClassRepository, StudentClassBusinessRules studentClassBusinessRules, IContextOperationService contextOperationService)
    {
        _studentClassRepository = studentClassRepository;
        _studentClassBusinessRules = studentClassBusinessRules;
        _contextOperationService = contextOperationService;
    }

    public async Task<StudentClass?> GetAsync(
        Expression<Func<StudentClass, bool>> predicate,
        Func<IQueryable<StudentClass>, IIncludableQueryable<StudentClass, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        StudentClass? studentClass = await _studentClassRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return studentClass;
    }

    public async Task<IPaginate<StudentClass>?> GetListAsync(
        Expression<Func<StudentClass, bool>>? predicate = null,
        Func<IQueryable<StudentClass>, IOrderedQueryable<StudentClass>>? orderBy = null,
        Func<IQueryable<StudentClass>, IIncludableQueryable<StudentClass, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<StudentClass> studentClassList = await _studentClassRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return studentClassList;
    }

    public async Task<StudentClass> AddAsync(StudentClass studentClass)
    {
        StudentClass addedStudentClass = await _studentClassRepository.AddAsync(studentClass);

        return addedStudentClass;
    }

    public async Task<StudentClass> UpdateAsync(StudentClass studentClass)
    {
        StudentClass updatedStudentClass = await _studentClassRepository.UpdateAsync(studentClass);

        return updatedStudentClass;
    }

    public async Task<StudentClass> DeleteAsync(StudentClass studentClass, bool permanent = false)
    {
        StudentClass deletedStudentClass = await _studentClassRepository.DeleteAsync(studentClass);

        return deletedStudentClass;
    }

    public async Task<int> GetAllContentCountForActiveStudent()
    {
        ICollection<Guid> activeStudentClasses = await _contextOperationService.GetStudentClassesFromContext();

        StudentClass lecture = await _studentClassRepository.GetAsync(
                predicate: l => activeStudentClasses.Contains(l.Id),
                include: l => l.
                    Include(l=>l.ClassLectures).
                    ThenInclude(l => l.Lecture)
                    .ThenInclude(l => l.LectureCourses)
                   .ThenInclude(lc => lc.Course)
                   .ThenInclude(c => c.CourseContents)
                   .ThenInclude(cc => cc.Content));

        var contentCount = lecture.ClassLectures.Select(l => l.Lecture.LectureCourses.Select(lc => lc.Course.CourseContents)).ToList().Count;
            

        return contentCount;
    }
}

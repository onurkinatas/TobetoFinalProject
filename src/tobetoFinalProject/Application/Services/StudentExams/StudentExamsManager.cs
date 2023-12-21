using Application.Features.StudentExams.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.StudentExams;

public class StudentExamsManager : IStudentExamsService
{
    private readonly IStudentExamRepository _studentExamRepository;
    private readonly StudentExamBusinessRules _studentExamBusinessRules;

    public StudentExamsManager(IStudentExamRepository studentExamRepository, StudentExamBusinessRules studentExamBusinessRules)
    {
        _studentExamRepository = studentExamRepository;
        _studentExamBusinessRules = studentExamBusinessRules;
    }

    public async Task<StudentExam?> GetAsync(
        Expression<Func<StudentExam, bool>> predicate,
        Func<IQueryable<StudentExam>, IIncludableQueryable<StudentExam, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        StudentExam? studentExam = await _studentExamRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return studentExam;
    }

    public async Task<IPaginate<StudentExam>?> GetListAsync(
        Expression<Func<StudentExam, bool>>? predicate = null,
        Func<IQueryable<StudentExam>, IOrderedQueryable<StudentExam>>? orderBy = null,
        Func<IQueryable<StudentExam>, IIncludableQueryable<StudentExam, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<StudentExam> studentExamList = await _studentExamRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return studentExamList;
    }

    public async Task<StudentExam> AddAsync(StudentExam studentExam)
    {
        StudentExam addedStudentExam = await _studentExamRepository.AddAsync(studentExam);

        return addedStudentExam;
    }

    public async Task<StudentExam> UpdateAsync(StudentExam studentExam)
    {
        StudentExam updatedStudentExam = await _studentExamRepository.UpdateAsync(studentExam);

        return updatedStudentExam;
    }

    public async Task<StudentExam> DeleteAsync(StudentExam studentExam, bool permanent = false)
    {
        StudentExam deletedStudentExam = await _studentExamRepository.DeleteAsync(studentExam);

        return deletedStudentExam;
    }
}

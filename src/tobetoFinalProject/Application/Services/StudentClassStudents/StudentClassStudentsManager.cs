using Application.Features.StudentClassStudents.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using Nest;
using System.Linq.Expressions;

namespace Application.Services.StudentClassStudents;

public class StudentClassStudentsManager : IStudentClassStudentsService
{
    private readonly IStudentClassStudentRepository _studentClassStudentRepository;
    private readonly StudentClassStudentBusinessRules _studentClassStudentBusinessRules;

    public StudentClassStudentsManager(IStudentClassStudentRepository studentClassStudentRepository, StudentClassStudentBusinessRules studentClassStudentBusinessRules)
    {
        _studentClassStudentRepository = studentClassStudentRepository;
        _studentClassStudentBusinessRules = studentClassStudentBusinessRules;
    }

    public async Task<StudentClassStudent?> GetAsync(
        Expression<Func<StudentClassStudent, bool>> predicate,
        Func<IQueryable<StudentClassStudent>, IIncludableQueryable<StudentClassStudent, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        StudentClassStudent? studentClassStudent = await _studentClassStudentRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return studentClassStudent;
    }

    public async Task<IPaginate<StudentClassStudent>?> GetListAsync(
        Expression<Func<StudentClassStudent, bool>>? predicate = null,
        Func<IQueryable<StudentClassStudent>, IOrderedQueryable<StudentClassStudent>>? orderBy = null,
        Func<IQueryable<StudentClassStudent>, IIncludableQueryable<StudentClassStudent, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<StudentClassStudent> studentClassStudentList = await _studentClassStudentRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return studentClassStudentList;
    }

    public async Task<StudentClassStudent> AddAsync(StudentClassStudent studentClassStudent)
    {
        StudentClassStudent addedStudentClassStudent = await _studentClassStudentRepository.AddAsync(studentClassStudent);

        return addedStudentClassStudent;
    }

    public async Task<StudentClassStudent> UpdateAsync(StudentClassStudent studentClassStudent)
    {
        StudentClassStudent updatedStudentClassStudent = await _studentClassStudentRepository.UpdateAsync(studentClassStudent);

        return updatedStudentClassStudent;
    }

    public async Task<StudentClassStudent> DeleteAsync(StudentClassStudent studentClassStudent, bool permanent = false)
    {
        StudentClassStudent deletedStudentClassStudent = await _studentClassStudentRepository.DeleteAsync(studentClassStudent);

        return deletedStudentClassStudent;
    }
    public ICollection<StudentClassStudent> GetAllWithoutPaginate(Expression<Func<StudentClassStudent, bool>> filter = null)
    {
        var studentClassStudents=  _studentClassStudentRepository.GetAllWithoutPaginate(filter);
        return studentClassStudents;
    }
}

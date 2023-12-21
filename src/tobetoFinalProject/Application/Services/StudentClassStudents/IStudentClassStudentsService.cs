using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.StudentClassStudents;

public interface IStudentClassStudentsService
{
    Task<StudentClassStudent?> GetAsync(
        Expression<Func<StudentClassStudent, bool>> predicate,
        Func<IQueryable<StudentClassStudent>, IIncludableQueryable<StudentClassStudent, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<StudentClassStudent>?> GetListAsync(
        Expression<Func<StudentClassStudent, bool>>? predicate = null,
        Func<IQueryable<StudentClassStudent>, IOrderedQueryable<StudentClassStudent>>? orderBy = null,
        Func<IQueryable<StudentClassStudent>, IIncludableQueryable<StudentClassStudent, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<StudentClassStudent> AddAsync(StudentClassStudent studentClassStudent);
    Task<StudentClassStudent> UpdateAsync(StudentClassStudent studentClassStudent);
    Task<StudentClassStudent> DeleteAsync(StudentClassStudent studentClassStudent, bool permanent = false);
}

using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.StudentExams;

public interface IStudentExamsService
{
    Task<StudentExam?> GetAsync(
        Expression<Func<StudentExam, bool>> predicate,
        Func<IQueryable<StudentExam>, IIncludableQueryable<StudentExam, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<StudentExam>?> GetListAsync(
        Expression<Func<StudentExam, bool>>? predicate = null,
        Func<IQueryable<StudentExam>, IOrderedQueryable<StudentExam>>? orderBy = null,
        Func<IQueryable<StudentExam>, IIncludableQueryable<StudentExam, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<StudentExam> AddAsync(StudentExam studentExam);
    Task<StudentExam> UpdateAsync(StudentExam studentExam);
    Task<StudentExam> DeleteAsync(StudentExam studentExam, bool permanent = false);
}

using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.StudentQuizResults;

public interface IStudentQuizResultsService
{
    Task<StudentQuizResult?> GetAsync(
        Expression<Func<StudentQuizResult, bool>> predicate,
        Func<IQueryable<StudentQuizResult>, IIncludableQueryable<StudentQuizResult, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<StudentQuizResult>?> GetListAsync(
        Expression<Func<StudentQuizResult, bool>>? predicate = null,
        Func<IQueryable<StudentQuizResult>, IOrderedQueryable<StudentQuizResult>>? orderBy = null,
        Func<IQueryable<StudentQuizResult>, IIncludableQueryable<StudentQuizResult, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<StudentQuizResult> AddAsync(StudentQuizResult studentQuizResult);
    Task<StudentQuizResult> UpdateAsync(StudentQuizResult studentQuizResult);
    Task<StudentQuizResult> DeleteAsync(StudentQuizResult studentQuizResult, bool permanent = false);
    Task<Task> UpdateQuizResultAsync(int quizId, Guid? studentId, int? optionId, int questionId);
}

using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.StudentQuizOptions;

public interface IStudentQuizOptionsService
{
    Task<StudentQuizOption?> GetAsync(
        Expression<Func<StudentQuizOption, bool>> predicate,
        Func<IQueryable<StudentQuizOption>, IIncludableQueryable<StudentQuizOption, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<StudentQuizOption>?> GetListAsync(
        Expression<Func<StudentQuizOption, bool>>? predicate = null,
        Func<IQueryable<StudentQuizOption>, IOrderedQueryable<StudentQuizOption>>? orderBy = null,
        Func<IQueryable<StudentQuizOption>, IIncludableQueryable<StudentQuizOption, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<StudentQuizOption> AddAsync(StudentQuizOption studentQuizOption);
    Task<StudentQuizOption> UpdateAsync(StudentQuizOption studentQuizOption);
    Task<StudentQuizOption> DeleteAsync(StudentQuizOption studentQuizOption, bool permanent = false);
}

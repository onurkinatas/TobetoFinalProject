using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.StudentAppeals;

public interface IStudentAppealsService
{
    Task<StudentAppeal?> GetAsync(
        Expression<Func<StudentAppeal, bool>> predicate,
        Func<IQueryable<StudentAppeal>, IIncludableQueryable<StudentAppeal, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<StudentAppeal>?> GetListAsync(
        Expression<Func<StudentAppeal, bool>>? predicate = null,
        Func<IQueryable<StudentAppeal>, IOrderedQueryable<StudentAppeal>>? orderBy = null,
        Func<IQueryable<StudentAppeal>, IIncludableQueryable<StudentAppeal, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<StudentAppeal> AddAsync(StudentAppeal studentAppeal);
    Task<StudentAppeal> UpdateAsync(StudentAppeal studentAppeal);
    Task<StudentAppeal> DeleteAsync(StudentAppeal studentAppeal, bool permanent = false);
}

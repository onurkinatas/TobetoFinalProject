using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ClassExams;

public interface IClassExamsService
{
    Task<ClassExam?> GetAsync(
        Expression<Func<ClassExam, bool>> predicate,
        Func<IQueryable<ClassExam>, IIncludableQueryable<ClassExam, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<ClassExam>?> GetListAsync(
        Expression<Func<ClassExam, bool>>? predicate = null,
        Func<IQueryable<ClassExam>, IOrderedQueryable<ClassExam>>? orderBy = null,
        Func<IQueryable<ClassExam>, IIncludableQueryable<ClassExam, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<ClassExam> AddAsync(ClassExam classExam);
    Task<ClassExam> UpdateAsync(ClassExam classExam);
    Task<ClassExam> DeleteAsync(ClassExam classExam, bool permanent = false);
}

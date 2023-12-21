using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.StudentStages;

public interface IStudentStagesService
{
    Task<StudentStage?> GetAsync(
        Expression<Func<StudentStage, bool>> predicate,
        Func<IQueryable<StudentStage>, IIncludableQueryable<StudentStage, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<StudentStage>?> GetListAsync(
        Expression<Func<StudentStage, bool>>? predicate = null,
        Func<IQueryable<StudentStage>, IOrderedQueryable<StudentStage>>? orderBy = null,
        Func<IQueryable<StudentStage>, IIncludableQueryable<StudentStage, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<StudentStage> AddAsync(StudentStage studentStage);
    Task<StudentStage> UpdateAsync(StudentStage studentStage);
    Task<StudentStage> DeleteAsync(StudentStage studentStage, bool permanent = false);
}

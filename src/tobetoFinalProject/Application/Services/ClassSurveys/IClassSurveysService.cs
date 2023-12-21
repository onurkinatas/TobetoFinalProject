using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ClassSurveys;

public interface IClassSurveysService
{
    Task<ClassSurvey?> GetAsync(
        Expression<Func<ClassSurvey, bool>> predicate,
        Func<IQueryable<ClassSurvey>, IIncludableQueryable<ClassSurvey, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<ClassSurvey>?> GetListAsync(
        Expression<Func<ClassSurvey, bool>>? predicate = null,
        Func<IQueryable<ClassSurvey>, IOrderedQueryable<ClassSurvey>>? orderBy = null,
        Func<IQueryable<ClassSurvey>, IIncludableQueryable<ClassSurvey, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<ClassSurvey> AddAsync(ClassSurvey classSurvey);
    Task<ClassSurvey> UpdateAsync(ClassSurvey classSurvey);
    Task<ClassSurvey> DeleteAsync(ClassSurvey classSurvey, bool permanent = false);
}

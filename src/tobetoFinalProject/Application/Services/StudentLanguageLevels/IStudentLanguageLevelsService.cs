using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.StudentLanguageLevels;

public interface IStudentLanguageLevelsService
{
    Task<StudentLanguageLevel?> GetAsync(
        Expression<Func<StudentLanguageLevel, bool>> predicate,
        Func<IQueryable<StudentLanguageLevel>, IIncludableQueryable<StudentLanguageLevel, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<StudentLanguageLevel>?> GetListAsync(
        Expression<Func<StudentLanguageLevel, bool>>? predicate = null,
        Func<IQueryable<StudentLanguageLevel>, IOrderedQueryable<StudentLanguageLevel>>? orderBy = null,
        Func<IQueryable<StudentLanguageLevel>, IIncludableQueryable<StudentLanguageLevel, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<StudentLanguageLevel> AddAsync(StudentLanguageLevel studentLanguageLevel);
    Task<StudentLanguageLevel> UpdateAsync(StudentLanguageLevel studentLanguageLevel);
    Task<StudentLanguageLevel> DeleteAsync(StudentLanguageLevel studentLanguageLevel, bool permanent = false);
}

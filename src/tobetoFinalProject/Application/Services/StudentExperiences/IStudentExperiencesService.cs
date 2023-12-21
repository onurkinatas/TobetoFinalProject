using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.StudentExperiences;

public interface IStudentExperiencesService
{
    Task<StudentExperience?> GetAsync(
        Expression<Func<StudentExperience, bool>> predicate,
        Func<IQueryable<StudentExperience>, IIncludableQueryable<StudentExperience, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<StudentExperience>?> GetListAsync(
        Expression<Func<StudentExperience, bool>>? predicate = null,
        Func<IQueryable<StudentExperience>, IOrderedQueryable<StudentExperience>>? orderBy = null,
        Func<IQueryable<StudentExperience>, IIncludableQueryable<StudentExperience, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<StudentExperience> AddAsync(StudentExperience studentExperience);
    Task<StudentExperience> UpdateAsync(StudentExperience studentExperience);
    Task<StudentExperience> DeleteAsync(StudentExperience studentExperience, bool permanent = false);
}

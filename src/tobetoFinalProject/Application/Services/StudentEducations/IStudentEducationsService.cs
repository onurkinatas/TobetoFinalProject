using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.StudentEducations;

public interface IStudentEducationsService
{
    Task<StudentEducation?> GetAsync(
        Expression<Func<StudentEducation, bool>> predicate,
        Func<IQueryable<StudentEducation>, IIncludableQueryable<StudentEducation, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<StudentEducation>?> GetListAsync(
        Expression<Func<StudentEducation, bool>>? predicate = null,
        Func<IQueryable<StudentEducation>, IOrderedQueryable<StudentEducation>>? orderBy = null,
        Func<IQueryable<StudentEducation>, IIncludableQueryable<StudentEducation, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<StudentEducation> AddAsync(StudentEducation studentEducation);
    Task<StudentEducation> UpdateAsync(StudentEducation studentEducation);
    Task<StudentEducation> DeleteAsync(StudentEducation studentEducation, bool permanent = false);
}

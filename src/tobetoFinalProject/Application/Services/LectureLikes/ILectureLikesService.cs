using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.LectureLikes;

public interface ILectureLikesService
{
    Task<LectureLike?> GetAsync(
        Expression<Func<LectureLike, bool>> predicate,
        Func<IQueryable<LectureLike>, IIncludableQueryable<LectureLike, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<LectureLike>?> GetListAsync(
        Expression<Func<LectureLike, bool>>? predicate = null,
        Func<IQueryable<LectureLike>, IOrderedQueryable<LectureLike>>? orderBy = null,
        Func<IQueryable<LectureLike>, IIncludableQueryable<LectureLike, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<LectureLike> AddAsync(LectureLike lectureLike);
    Task<LectureLike> UpdateAsync(LectureLike lectureLike);
    Task<LectureLike> DeleteAsync(LectureLike lectureLike, bool permanent = false);
}

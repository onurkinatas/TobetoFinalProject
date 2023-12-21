using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Lectures;

public interface ILecturesService
{
    Task<Lecture?> GetAsync(
        Expression<Func<Lecture, bool>> predicate,
        Func<IQueryable<Lecture>, IIncludableQueryable<Lecture, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Lecture>?> GetListAsync(
        Expression<Func<Lecture, bool>>? predicate = null,
        Func<IQueryable<Lecture>, IOrderedQueryable<Lecture>>? orderBy = null,
        Func<IQueryable<Lecture>, IIncludableQueryable<Lecture, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Lecture> AddAsync(Lecture lecture);
    Task<Lecture> UpdateAsync(Lecture lecture);
    Task<Lecture> DeleteAsync(Lecture lecture, bool permanent = false);
}

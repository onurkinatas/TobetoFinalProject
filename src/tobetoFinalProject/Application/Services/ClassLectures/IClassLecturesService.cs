using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ClassLectures;

public interface IClassLecturesService
{
    Task<ClassLecture?> GetAsync(
        Expression<Func<ClassLecture, bool>> predicate,
        Func<IQueryable<ClassLecture>, IIncludableQueryable<ClassLecture, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<ClassLecture>?> GetListAsync(
        Expression<Func<ClassLecture, bool>>? predicate = null,
        Func<IQueryable<ClassLecture>, IOrderedQueryable<ClassLecture>>? orderBy = null,
        Func<IQueryable<ClassLecture>, IIncludableQueryable<ClassLecture, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<ClassLecture> AddAsync(ClassLecture classLecture);
    Task<ClassLecture> UpdateAsync(ClassLecture classLecture);
    Task<ClassLecture> DeleteAsync(ClassLecture classLecture, bool permanent = false);
}

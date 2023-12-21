using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.LectureSpentTimes;

public interface ILectureSpentTimesService
{
    Task<LectureSpentTime?> GetAsync(
        Expression<Func<LectureSpentTime, bool>> predicate,
        Func<IQueryable<LectureSpentTime>, IIncludableQueryable<LectureSpentTime, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<LectureSpentTime>?> GetListAsync(
        Expression<Func<LectureSpentTime, bool>>? predicate = null,
        Func<IQueryable<LectureSpentTime>, IOrderedQueryable<LectureSpentTime>>? orderBy = null,
        Func<IQueryable<LectureSpentTime>, IIncludableQueryable<LectureSpentTime, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<LectureSpentTime> AddAsync(LectureSpentTime lectureSpentTime);
    Task<LectureSpentTime> UpdateAsync(LectureSpentTime lectureSpentTime);
    Task<LectureSpentTime> DeleteAsync(LectureSpentTime lectureSpentTime, bool permanent = false);
}

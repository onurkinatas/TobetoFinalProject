using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.LectureViews;

public interface ILectureViewsService
{
    Task<LectureView?> GetAsync(
        Expression<Func<LectureView, bool>> predicate,
        Func<IQueryable<LectureView>, IIncludableQueryable<LectureView, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<LectureView>?> GetListAsync(
        Expression<Func<LectureView, bool>>? predicate = null,
        Func<IQueryable<LectureView>, IOrderedQueryable<LectureView>>? orderBy = null,
        Func<IQueryable<LectureView>, IIncludableQueryable<LectureView, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<LectureView> AddAsync(LectureView lectureView);
    Task<LectureView> UpdateAsync(LectureView lectureView);
    Task<LectureView> DeleteAsync(LectureView lectureView, bool permanent = false);
    Task<int> ContentViewedByLectureId(Guid lectureId,Guid studentId);
    int ContentViewedCountByLectureId(Guid lectureId, Guid studentId);
}

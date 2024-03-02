using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.LectureCompletionConditions;

public interface ILectureCompletionConditionsService
{
    Task<LectureCompletionCondition?> GetAsync(
        Expression<Func<LectureCompletionCondition, bool>> predicate,
        Func<IQueryable<LectureCompletionCondition>, IIncludableQueryable<LectureCompletionCondition, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<LectureCompletionCondition>?> GetListAsync(
        Expression<Func<LectureCompletionCondition, bool>>? predicate = null,
        Func<IQueryable<LectureCompletionCondition>, IOrderedQueryable<LectureCompletionCondition>>? orderBy = null,
        Func<IQueryable<LectureCompletionCondition>, IIncludableQueryable<LectureCompletionCondition, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<LectureCompletionCondition> AddAsync(LectureCompletionCondition lectureCompletionCondition);
    Task<LectureCompletionCondition> UpdateAsync(LectureCompletionCondition lectureCompletionCondition);
    Task<LectureCompletionCondition> DeleteAsync(LectureCompletionCondition lectureCompletionCondition, bool permanent = false);
    int CompletionPercentageCalculator(int lectureViewCount,int contentCount);

    Task ImpactOfLectureView(Guid lectureId, Guid studentId, CancellationToken cancellationToken);
}

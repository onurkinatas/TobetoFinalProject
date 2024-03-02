using Application.Features.LectureCompletionConditions.Rules;
using Application.Services.Lectures;
using Application.Services.LectureViews;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using System.Threading;

namespace Application.Services.LectureCompletionConditions;

public class LectureCompletionConditionsManager : ILectureCompletionConditionsService
{
    private readonly ILectureCompletionConditionRepository _lectureCompletionConditionRepository;
    private readonly LectureCompletionConditionBusinessRules _lectureCompletionConditionBusinessRules;
    private readonly ILectureViewsService _lectureViewsService;
    private readonly ILecturesService _lecturesService;
    public LectureCompletionConditionsManager(ILectureCompletionConditionRepository lectureCompletionConditionRepository, LectureCompletionConditionBusinessRules lectureCompletionConditionBusinessRules, ILecturesService lecturesService, ILectureViewsService lectureViewsService)
    {
        _lectureCompletionConditionRepository = lectureCompletionConditionRepository;
        _lectureCompletionConditionBusinessRules = lectureCompletionConditionBusinessRules;
        _lecturesService = lecturesService;
        _lectureViewsService = lectureViewsService;
    }

    public async Task<LectureCompletionCondition?> GetAsync(
        Expression<Func<LectureCompletionCondition, bool>> predicate,
        Func<IQueryable<LectureCompletionCondition>, IIncludableQueryable<LectureCompletionCondition, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        LectureCompletionCondition? lectureCompletionCondition = await _lectureCompletionConditionRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return lectureCompletionCondition;
    }

    public async Task<IPaginate<LectureCompletionCondition>?> GetListAsync(
        Expression<Func<LectureCompletionCondition, bool>>? predicate = null,
        Func<IQueryable<LectureCompletionCondition>, IOrderedQueryable<LectureCompletionCondition>>? orderBy = null,
        Func<IQueryable<LectureCompletionCondition>, IIncludableQueryable<LectureCompletionCondition, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<LectureCompletionCondition> lectureCompletionConditionList = await _lectureCompletionConditionRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return lectureCompletionConditionList;
    }

    public async Task<LectureCompletionCondition> AddAsync(LectureCompletionCondition lectureCompletionCondition)
    {
        LectureCompletionCondition addedLectureCompletionCondition = await _lectureCompletionConditionRepository.AddAsync(lectureCompletionCondition);

        return addedLectureCompletionCondition;
    }

    public async Task<LectureCompletionCondition> UpdateAsync(LectureCompletionCondition lectureCompletionCondition)
    {
        LectureCompletionCondition updatedLectureCompletionCondition = await _lectureCompletionConditionRepository.UpdateAsync(lectureCompletionCondition);

        return updatedLectureCompletionCondition;
    }

    public async Task<LectureCompletionCondition> DeleteAsync(LectureCompletionCondition lectureCompletionCondition, bool permanent = false)
    {
        LectureCompletionCondition deletedLectureCompletionCondition = await _lectureCompletionConditionRepository.DeleteAsync(lectureCompletionCondition);

        return deletedLectureCompletionCondition;
    }

    public int CompletionPercentageCalculator(int lectureViewCount, int contentCount) => (lectureViewCount * 100) / contentCount;
    public async Task ImpactOfLectureView(Guid lectureId, Guid studentId,CancellationToken cancellationToken)
    {
        LectureCompletionCondition? doesExistLectureCompletionCondition = await _lectureCompletionConditionRepository.GetAsync(predicate: lcc => lcc.LectureId == lectureId
        && lcc.StudentId == studentId);

        int contentCount = await _lecturesService.GetAllContentCountByLectureId(lectureId, cancellationToken);
        int lectureViewCount = await _lectureViewsService.ContentViewedByLectureId(lectureId, studentId);
        int completionPercentage = CompletionPercentageCalculator(lectureViewCount, contentCount);

        if (doesExistLectureCompletionCondition is null)
        {
            await _lectureCompletionConditionRepository.AddAsync(new LectureCompletionCondition { StudentId = studentId, LectureId = lectureId, CompletionPercentage = completionPercentage });
        }

        else if (doesExistLectureCompletionCondition is not null)
        {
            doesExistLectureCompletionCondition.CompletionPercentage = completionPercentage;
            await _lectureCompletionConditionRepository.UpdateAsync(doesExistLectureCompletionCondition);
        }
    }
}

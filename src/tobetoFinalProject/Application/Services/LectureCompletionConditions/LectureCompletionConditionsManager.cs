using Application.Features.LectureCompletionConditions.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.LectureCompletionConditions;

public class LectureCompletionConditionsManager : ILectureCompletionConditionsService
{
    private readonly ILectureCompletionConditionRepository _lectureCompletionConditionRepository;
    private readonly LectureCompletionConditionBusinessRules _lectureCompletionConditionBusinessRules;

    public LectureCompletionConditionsManager(ILectureCompletionConditionRepository lectureCompletionConditionRepository, LectureCompletionConditionBusinessRules lectureCompletionConditionBusinessRules)
    {
        _lectureCompletionConditionRepository = lectureCompletionConditionRepository;
        _lectureCompletionConditionBusinessRules = lectureCompletionConditionBusinessRules;
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

    public async Task<int> CompletionPercentageCalculator(int lectureViewCount, int contentCount) => (lectureViewCount * 100) / contentCount;
}

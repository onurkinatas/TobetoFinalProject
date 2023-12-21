using Application.Features.LectureCompletionConditions.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.LectureCompletionConditions.Rules;

public class LectureCompletionConditionBusinessRules : BaseBusinessRules
{
    private readonly ILectureCompletionConditionRepository _lectureCompletionConditionRepository;

    public LectureCompletionConditionBusinessRules(ILectureCompletionConditionRepository lectureCompletionConditionRepository)
    {
        _lectureCompletionConditionRepository = lectureCompletionConditionRepository;
    }

    public Task LectureCompletionConditionShouldExistWhenSelected(LectureCompletionCondition? lectureCompletionCondition)
    {
        if (lectureCompletionCondition == null)
            throw new BusinessException(LectureCompletionConditionsBusinessMessages.LectureCompletionConditionNotExists);
        return Task.CompletedTask;
    }

    public async Task LectureCompletionConditionIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        LectureCompletionCondition? lectureCompletionCondition = await _lectureCompletionConditionRepository.GetAsync(
            predicate: lcc => lcc.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await LectureCompletionConditionShouldExistWhenSelected(lectureCompletionCondition);
    }
}
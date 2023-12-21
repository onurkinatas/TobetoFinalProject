using Application.Features.LectureSpentTimes.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.LectureSpentTimes.Rules;

public class LectureSpentTimeBusinessRules : BaseBusinessRules
{
    private readonly ILectureSpentTimeRepository _lectureSpentTimeRepository;

    public LectureSpentTimeBusinessRules(ILectureSpentTimeRepository lectureSpentTimeRepository)
    {
        _lectureSpentTimeRepository = lectureSpentTimeRepository;
    }

    public Task LectureSpentTimeShouldExistWhenSelected(LectureSpentTime? lectureSpentTime)
    {
        if (lectureSpentTime == null)
            throw new BusinessException(LectureSpentTimesBusinessMessages.LectureSpentTimeNotExists);
        return Task.CompletedTask;
    }

    public async Task LectureSpentTimeIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        LectureSpentTime? lectureSpentTime = await _lectureSpentTimeRepository.GetAsync(
            predicate: lst => lst.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await LectureSpentTimeShouldExistWhenSelected(lectureSpentTime);
    }
}
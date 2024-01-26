using Application.Features.LectureViews.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.LectureViews.Rules;

public class LectureViewBusinessRules : BaseBusinessRules
{
    private readonly ILectureViewRepository _lectureViewRepository;

    public LectureViewBusinessRules(ILectureViewRepository lectureViewRepository)
    {
        _lectureViewRepository = lectureViewRepository;
    }

    public Task LectureViewShouldExistWhenSelected(LectureView? lectureView)
    {
        if (lectureView == null)
            throw new BusinessException(LectureViewsBusinessMessages.LectureViewNotExists);
        return Task.CompletedTask;
    }

    public async Task LectureViewIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        LectureView? lectureView = await _lectureViewRepository.GetAsync(
            predicate: lv => lv.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await LectureViewShouldExistWhenSelected(lectureView);
    }
}
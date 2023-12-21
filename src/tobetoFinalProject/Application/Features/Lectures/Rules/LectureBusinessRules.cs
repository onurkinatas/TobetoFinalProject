using Application.Features.Lectures.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Lectures.Rules;

public class LectureBusinessRules : BaseBusinessRules
{
    private readonly ILectureRepository _lectureRepository;

    public LectureBusinessRules(ILectureRepository lectureRepository)
    {
        _lectureRepository = lectureRepository;
    }

    public Task LectureShouldExistWhenSelected(Lecture? lecture)
    {
        if (lecture == null)
            throw new BusinessException(LecturesBusinessMessages.LectureNotExists);
        return Task.CompletedTask;
    }

    public async Task LectureIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Lecture? lecture = await _lectureRepository.GetAsync(
            predicate: l => l.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await LectureShouldExistWhenSelected(lecture);
    }
}
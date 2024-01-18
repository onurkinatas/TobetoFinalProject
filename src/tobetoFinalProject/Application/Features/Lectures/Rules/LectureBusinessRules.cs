using Application.Features.Exams.Constants;
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

    public Task LectureShouldNotExist(Lecture? lecture)
    {
        if (lecture != null)
            throw new BusinessException(LecturesBusinessMessages.LectureNameExists);
        return Task.CompletedTask;
    }
    public async Task DistrictNameShouldNotExist(Lecture lecture, CancellationToken cancellationToken)
    {
        Lecture? controlLecture = await _lectureRepository.GetAsync(
            predicate: a => a.Name == lecture.Name,
            enableTracking: false, //Entity Framework'te "tracking" veya "izleme" (tracking) terimi, bir veri nesnesinin (entity) durumunu                          takip etme ve bu durumun veritabanýna nasýl yansýtýlacaðýný belirleme sürecini ifade eder.
            cancellationToken: cancellationToken //asenkron iþlemlerin iptal edilmesine olanak saðlar(Örnek çok uzun süren bir iþlemde)
            );
        await LectureShouldNotExist(controlLecture);
    }
}
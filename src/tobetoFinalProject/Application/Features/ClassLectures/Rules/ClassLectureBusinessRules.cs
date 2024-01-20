using Application.Features.ClassLectures.Constants;
using Application.Features.ClassLectures.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.ClassLectures.Rules;

public class ClassLectureBusinessRules : BaseBusinessRules
{
    private readonly IClassLectureRepository _classLectureRepository;

    public ClassLectureBusinessRules(IClassLectureRepository classLectureRepository)
    {
        _classLectureRepository = classLectureRepository;
    }
    public async Task ClassLectureShouldNotExistsWhenInsert(Guid classId, Guid announcementId)
    {
        bool doesExists = await _classLectureRepository
            .AnyAsync(predicate: ca => ca.LectureId == announcementId && ca.StudentClassId == classId, enableTracking: false);
        if (doesExists)
            throw new BusinessException(ClassLecturesBusinessMessages.ClassLectureAlreadyExists);
    }
    public async Task ClassLectureShouldNotExistsWhenUpdate(Guid classId, Guid announcementId)
    {
        bool doesExists = await _classLectureRepository
            .AnyAsync(predicate: ca => ca.LectureId == announcementId && ca.StudentClassId == classId, enableTracking: false);
        if (doesExists)
            throw new BusinessException(ClassLecturesBusinessMessages.ClassLectureAlreadyExists);
    }
    public Task ClassLectureShouldExistWhenSelected(ClassLecture? classLecture)
    {
        if (classLecture == null)
            throw new BusinessException(ClassLecturesBusinessMessages.ClassLectureNotExists);
        return Task.CompletedTask;
    }

    public async Task ClassLectureIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        ClassLecture? classLecture = await _classLectureRepository.GetAsync(
            predicate: cl => cl.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ClassLectureShouldExistWhenSelected(classLecture);
    }
}
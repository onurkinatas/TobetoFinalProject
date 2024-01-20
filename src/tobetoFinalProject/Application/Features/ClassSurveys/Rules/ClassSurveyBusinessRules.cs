using Application.Features.ClassSurveys.Constants;
using Application.Features.ClassSurveys.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.ClassSurveys.Rules;

public class ClassSurveyBusinessRules : BaseBusinessRules
{
    private readonly IClassSurveyRepository _classSurveyRepository;

    public ClassSurveyBusinessRules(IClassSurveyRepository classSurveyRepository)
    {
        _classSurveyRepository = classSurveyRepository;
    }
    public async Task ClassSurveyShouldNotExistsWhenInsert(Guid classId, Guid announcementId)
    {
        bool doesExists = await _classSurveyRepository
            .AnyAsync(predicate: ca => ca.SurveyId == announcementId && ca.StudentClassId == classId, enableTracking: false);
        if (doesExists)
            throw new BusinessException(ClassSurveysBusinessMessages.ClassSurveyAlreadyExists);
    }
    public async Task ClassSurveyShouldNotExistsWhenUpdate(Guid classId, Guid announcementId)
    {
        bool doesExists = await _classSurveyRepository
            .AnyAsync(predicate: ca => ca.SurveyId == announcementId && ca.StudentClassId == classId, enableTracking: false);
        if (doesExists)
            throw new BusinessException(ClassSurveysBusinessMessages.ClassSurveyAlreadyExists);
    }
    public Task ClassSurveyShouldExistWhenSelected(ClassSurvey? classSurvey)
    {
        if (classSurvey == null)
            throw new BusinessException(ClassSurveysBusinessMessages.ClassSurveyNotExists);
        return Task.CompletedTask;
    }

    public async Task ClassSurveyIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        ClassSurvey? classSurvey = await _classSurveyRepository.GetAsync(
            predicate: cs => cs.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ClassSurveyShouldExistWhenSelected(classSurvey);
    }
}
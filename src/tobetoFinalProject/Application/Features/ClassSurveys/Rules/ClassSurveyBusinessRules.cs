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
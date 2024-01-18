using Application.Features.Languages.Constants;
using Application.Features.Surveys.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Surveys.Rules;

public class SurveyBusinessRules : BaseBusinessRules
{
    private readonly ISurveyRepository _surveyRepository;

    public SurveyBusinessRules(ISurveyRepository surveyRepository)
    {
        _surveyRepository = surveyRepository;
    }

    public Task SurveyShouldExistWhenSelected(Survey? survey)
    {
        if (survey == null)
            throw new BusinessException(SurveysBusinessMessages.SurveyNotExists);
        return Task.CompletedTask;
    }

    public async Task SurveyIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Survey? survey = await _surveyRepository.GetAsync(
            predicate: s => s.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await SurveyShouldExistWhenSelected(survey);
    }

    public Task SurveyShouldNotExist(Survey? survey)
    {
        if (survey != null)
            throw new BusinessException(SurveysBusinessMessages.SurveyNameExists);
        return Task.CompletedTask;
    }
    public async Task SurveyNameShouldNotExist(Survey survey, CancellationToken cancellationToken)
    {
        Survey? controlSurvey = await _surveyRepository.GetAsync(
            predicate: a => a.Name == survey.Name,
            enableTracking: false,
            cancellationToken: cancellationToken
            );
        await SurveyShouldNotExist(controlSurvey);
    }
}
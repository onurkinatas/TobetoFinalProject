using Application.Features.GeneralQuizs.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.GeneralQuizs.Rules;

public class GeneralQuizBusinessRules : BaseBusinessRules
{
    private readonly IGeneralQuizRepository _generalQuizRepository;

    public GeneralQuizBusinessRules(IGeneralQuizRepository generalQuizRepository)
    {
        _generalQuizRepository = generalQuizRepository;
    }

    public Task GeneralQuizShouldExistWhenSelected(GeneralQuiz? generalQuiz)
    {
        if (generalQuiz == null)
            throw new BusinessException(GeneralQuizsBusinessMessages.GeneralQuizNotExists);
        return Task.CompletedTask;
    }

    public async Task GeneralQuizIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        GeneralQuiz? generalQuiz = await _generalQuizRepository.GetAsync(
            predicate: gq => gq.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await GeneralQuizShouldExistWhenSelected(generalQuiz);
    }
}
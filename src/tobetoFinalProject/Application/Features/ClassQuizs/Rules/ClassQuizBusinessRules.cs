using Application.Features.ClassQuizs.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.ClassQuizs.Rules;

public class ClassQuizBusinessRules : BaseBusinessRules
{
    private readonly IClassQuizRepository _classQuizRepository;

    public ClassQuizBusinessRules(IClassQuizRepository classQuizRepository)
    {
        _classQuizRepository = classQuizRepository;
    }

    public Task ClassQuizShouldExistWhenSelected(ClassQuiz? classQuiz)
    {
        if (classQuiz == null)
            throw new BusinessException(ClassQuizsBusinessMessages.ClassQuizNotExists);
        return Task.CompletedTask;
    }

    public async Task ClassQuizIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        ClassQuiz? classQuiz = await _classQuizRepository.GetAsync(
            predicate: cq => cq.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ClassQuizShouldExistWhenSelected(classQuiz);
    }
}
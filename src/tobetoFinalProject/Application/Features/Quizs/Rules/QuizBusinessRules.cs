using Application.Features.Quizs.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Quizs.Rules;

public class QuizBusinessRules : BaseBusinessRules
{
    private readonly IQuizRepository _quizRepository;

    public QuizBusinessRules(IQuizRepository quizRepository)
    {
        _quizRepository = quizRepository;
    }

    public Task QuizShouldExistWhenSelected(Quiz? quiz)
    {
        if (quiz == null)
            throw new BusinessException(QuizsBusinessMessages.QuizNotExists);
        return Task.CompletedTask;
    }

    public async Task QuizIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        Quiz? quiz = await _quizRepository.GetAsync(
            predicate: q => q.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await QuizShouldExistWhenSelected(quiz);
    }
}
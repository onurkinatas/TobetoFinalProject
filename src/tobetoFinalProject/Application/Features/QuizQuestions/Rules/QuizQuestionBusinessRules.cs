using Application.Features.QuizQuestions.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.QuizQuestions.Rules;

public class QuizQuestionBusinessRules : BaseBusinessRules
{
    private readonly IQuizQuestionRepository _quizQuestionRepository;

    public QuizQuestionBusinessRules(IQuizQuestionRepository quizQuestionRepository)
    {
        _quizQuestionRepository = quizQuestionRepository;
    }

    public Task QuizQuestionShouldExistWhenSelected(QuizQuestion? quizQuestion)
    {
        if (quizQuestion == null)
            throw new BusinessException(QuizQuestionsBusinessMessages.QuizQuestionNotExists);
        return Task.CompletedTask;
    }

    public async Task QuizQuestionIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        QuizQuestion? quizQuestion = await _quizQuestionRepository.GetAsync(
            predicate: qq => qq.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await QuizQuestionShouldExistWhenSelected(quizQuestion);
    }
}
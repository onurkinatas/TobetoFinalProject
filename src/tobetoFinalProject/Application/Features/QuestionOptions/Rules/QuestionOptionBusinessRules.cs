using Application.Features.QuestionOptions.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.QuestionOptions.Rules;

public class QuestionOptionBusinessRules : BaseBusinessRules
{
    private readonly IQuestionOptionRepository _questionOptionRepository;

    public QuestionOptionBusinessRules(IQuestionOptionRepository questionOptionRepository)
    {
        _questionOptionRepository = questionOptionRepository;
    }

    public Task QuestionOptionShouldExistWhenSelected(QuestionOption? questionOption)
    {
        if (questionOption == null)
            throw new BusinessException(QuestionOptionsBusinessMessages.QuestionOptionNotExists);
        return Task.CompletedTask;
    }

    public async Task QuestionOptionIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        QuestionOption? questionOption = await _questionOptionRepository.GetAsync(
            predicate: qo => qo.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await QuestionOptionShouldExistWhenSelected(questionOption);
    }
}
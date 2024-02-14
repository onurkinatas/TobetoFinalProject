using Application.Features.Questions.Constants;
using Application.Services.QuestionOptions;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Questions.Rules;

public class QuestionBusinessRules : BaseBusinessRules
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IQuestionOptionsService _questionOptionsService;

    public QuestionBusinessRules(IQuestionRepository questionRepository, IQuestionOptionsService questionOptionsService)
    {
        _questionRepository = questionRepository;
        _questionOptionsService = questionOptionsService;
    }

    public Task QuestionShouldExistWhenSelected(Question? question)
    {
        if (question == null)
            throw new BusinessException(QuestionsBusinessMessages.QuestionNotExists);
        return Task.CompletedTask;
    }

    public async Task QuestionIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        Question? question = await _questionRepository.GetAsync(
            predicate: q => q.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await QuestionShouldExistWhenSelected(question);
    }
    public async Task<Task> QuestionOptionsCountMustBeLessThanSevenWhenInsert(int questionOptionCount)
    {
        if (questionOptionCount < 7)
            return Task.CompletedTask;
        throw new BusinessException(QuestionsBusinessMessages.QuestionOptionsLessThanSeven);
    }
    public  Task QuestionOptionsMustBeDifferent(ICollection<QuestionOption> questionOptions)
    {
        var optionIds = questionOptions.Select(qo => qo.OptionId);

        if (optionIds.Distinct().Count() !=  optionIds.Count())
            throw new BusinessException(QuestionsBusinessMessages.QuestionOptionsMustBeDifferent);
        
        return Task.CompletedTask;
    }
    
}
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
    
    public Task PoolQuestionsMustBeDifferentWhenInsert(ICollection<PoolQuestion> poolQuestions)
    {
        var poolIds = poolQuestions.Select(qo => qo.PoolId);

        if (poolIds.Distinct().Count() != poolIds.Count())
            throw new BusinessException(QuestionsBusinessMessages.PoolQuestionsMustBeDifferent);

        return Task.CompletedTask;
    }
    public Task QuestionOptionsHaveToContainCorrectOptionId(ICollection<QuestionOption> questionOptions,int correctOptionId)
    {
        bool doesExist = questionOptions.Any(qo => qo.OptionId==correctOptionId);

        if (doesExist is false)
            throw new BusinessException(QuestionsBusinessMessages.QuestionOptionsHaveToContainCorrectOption);

        return Task.CompletedTask;
    }
    
    public  Task QuestionOptionsMustBeDifferentWhenInsert(ICollection<QuestionOption> questionOptions)
    {
        var optionIds = questionOptions.Select(qo => qo.OptionId);

        if (optionIds.Distinct().Count() !=  optionIds.Count())
            throw new BusinessException(QuestionsBusinessMessages.QuestionOptionsMustBeDifferent);
        
        return Task.CompletedTask;
    }
    
}
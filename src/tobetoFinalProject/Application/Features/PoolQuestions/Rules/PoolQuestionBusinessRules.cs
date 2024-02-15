using Application.Features.PoolQuestions.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.PoolQuestions.Rules;

public class PoolQuestionBusinessRules : BaseBusinessRules
{
    private readonly IPoolQuestionRepository _poolQuestionRepository;

    public PoolQuestionBusinessRules(IPoolQuestionRepository poolQuestionRepository)
    {
        _poolQuestionRepository = poolQuestionRepository;
    }

    public Task PoolQuestionShouldExistWhenSelected(PoolQuestion? poolQuestion)
    {
        if (poolQuestion == null)
            throw new BusinessException(PoolQuestionsBusinessMessages.PoolQuestionNotExists);
        return Task.CompletedTask;
    }

    public async Task PoolQuestionIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        PoolQuestion? poolQuestion = await _poolQuestionRepository.GetAsync(
            predicate: pq => pq.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await PoolQuestionShouldExistWhenSelected(poolQuestion);
    }
    public async Task<Task> TotalQuestionCountMustBeLessThanPoolCount(int totalQuestionCount, int poolCount)
    {
        if (totalQuestionCount > poolCount)
            throw new BusinessException("Malesef Havuzunuzda " + poolCount + " Soru Var." + " Siz ise " + totalQuestionCount + " Soru Denediniz");
        return Task.CompletedTask;
    }
}
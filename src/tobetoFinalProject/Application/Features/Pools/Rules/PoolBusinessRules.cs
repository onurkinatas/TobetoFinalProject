using Application.Features.Pools.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Pools.Rules;

public class PoolBusinessRules : BaseBusinessRules
{
    private readonly IPoolRepository _poolRepository;

    public PoolBusinessRules(IPoolRepository poolRepository)
    {
        _poolRepository = poolRepository;
    }

    public Task PoolShouldExistWhenSelected(Pool? pool)
    {
        if (pool == null)
            throw new BusinessException(PoolsBusinessMessages.PoolNotExists);
        return Task.CompletedTask;
    }

    public async Task PoolIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        Pool? pool = await _poolRepository.GetAsync(
            predicate: p => p.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await PoolShouldExistWhenSelected(pool);
    }
    public async Task<Task> TotalQuestionCountMustBeLessThanPoolCount(int totalQuestionCount,int poolCount)
    {
        if (totalQuestionCount > poolCount)
            throw new BusinessException("Malesef Havuzunuzda " + poolCount + "Soru Var."+"Siz ise" + totalQuestionCount +"Soru Denediniz");
        return Task.CompletedTask;
    }
    
}
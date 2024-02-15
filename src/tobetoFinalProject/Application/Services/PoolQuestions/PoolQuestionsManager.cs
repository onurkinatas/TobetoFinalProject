using Application.Features.PoolQuestions.Rules;
using Application.Features.Pools.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.PoolQuestions;

public class PoolQuestionsManager : IPoolQuestionsService
{
    private readonly IPoolQuestionRepository _poolQuestionRepository;
    private readonly PoolQuestionBusinessRules _poolQuestionBusinessRules;

    public PoolQuestionsManager(IPoolQuestionRepository poolQuestionRepository, PoolQuestionBusinessRules poolQuestionBusinessRules)
    {
        _poolQuestionRepository = poolQuestionRepository;
        _poolQuestionBusinessRules = poolQuestionBusinessRules;
    }

    public async Task<PoolQuestion?> GetAsync(
        Expression<Func<PoolQuestion, bool>> predicate,
        Func<IQueryable<PoolQuestion>, IIncludableQueryable<PoolQuestion, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        PoolQuestion? poolQuestion = await _poolQuestionRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return poolQuestion;
    }

    public async Task<IPaginate<PoolQuestion>?> GetListAsync(
        Expression<Func<PoolQuestion, bool>>? predicate = null,
        Func<IQueryable<PoolQuestion>, IOrderedQueryable<PoolQuestion>>? orderBy = null,
        Func<IQueryable<PoolQuestion>, IIncludableQueryable<PoolQuestion, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<PoolQuestion> poolQuestionList = await _poolQuestionRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return poolQuestionList;
    }

    public async Task<PoolQuestion> AddAsync(PoolQuestion poolQuestion)
    {
        PoolQuestion addedPoolQuestion = await _poolQuestionRepository.AddAsync(poolQuestion);

        return addedPoolQuestion;
    }

    public async Task<PoolQuestion> UpdateAsync(PoolQuestion poolQuestion)
    {
        PoolQuestion updatedPoolQuestion = await _poolQuestionRepository.UpdateAsync(poolQuestion);

        return updatedPoolQuestion;
    }

    public async Task<PoolQuestion> DeleteAsync(PoolQuestion poolQuestion, bool permanent = false)
    {
        PoolQuestion deletedPoolQuestion = await _poolQuestionRepository.DeleteAsync(poolQuestion);

        return deletedPoolQuestion;
    }
    public async Task<List<int>> RandomQuestionGenerator(int totalQuestionCount, int poolId)
    {
        List<PoolQuestion> poolQuestions = await _poolQuestionRepository.GetAll(p => p.PoolId == poolId);

        await _poolQuestionBusinessRules.TotalQuestionCountMustBeLessThanPoolCount(totalQuestionCount, poolQuestions.Count);

        List<int> randomQuestionIds = poolQuestions
                  .OrderBy(q => Guid.NewGuid())  
                  .Select(q => q.QuestionId)     
                  .Distinct()                    
                  .Take(totalQuestionCount)      
                  .ToList();
        return randomQuestionIds;

    }
}

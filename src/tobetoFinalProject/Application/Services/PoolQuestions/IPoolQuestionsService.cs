using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.PoolQuestions;

public interface IPoolQuestionsService
{
    Task<PoolQuestion?> GetAsync(
        Expression<Func<PoolQuestion, bool>> predicate,
        Func<IQueryable<PoolQuestion>, IIncludableQueryable<PoolQuestion, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<PoolQuestion>?> GetListAsync(
        Expression<Func<PoolQuestion, bool>>? predicate = null,
        Func<IQueryable<PoolQuestion>, IOrderedQueryable<PoolQuestion>>? orderBy = null,
        Func<IQueryable<PoolQuestion>, IIncludableQueryable<PoolQuestion, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<PoolQuestion> AddAsync(PoolQuestion poolQuestion);
    Task<PoolQuestion> UpdateAsync(PoolQuestion poolQuestion);
    Task<PoolQuestion> DeleteAsync(PoolQuestion poolQuestion, bool permanent = false);
    Task<List<int>> RandomQuestionGenerator(int totalQuestionCount, int poolId);
}

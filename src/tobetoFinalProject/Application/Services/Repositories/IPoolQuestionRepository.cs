using Domain.Entities;
using Core.Persistence.Repositories;
using System.Linq.Expressions;

namespace Application.Services.Repositories;

public interface IPoolQuestionRepository : IAsyncRepository<PoolQuestion, int>, IRepository<PoolQuestion, int>
{
    Task<List<PoolQuestion>> GetAll(Expression<Func<PoolQuestion, bool>> filter = null);
}
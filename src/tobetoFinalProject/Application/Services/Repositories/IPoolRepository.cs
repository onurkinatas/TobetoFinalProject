using Domain.Entities;
using Core.Persistence.Repositories;
using System.Linq.Expressions;

namespace Application.Services.Repositories;

public interface IPoolRepository : IAsyncRepository<Pool, int>, IRepository<Pool, int>
{
    Task<List<Pool>> GetAll(Expression<Func<Pool, bool>> filter = null);
}
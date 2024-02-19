using Domain.Entities;
using Core.Persistence.Repositories;
using System.Linq.Expressions;

namespace Application.Services.Repositories;

public interface ILectureLikeRepository : IAsyncRepository<LectureLike, Guid>, IRepository<LectureLike, Guid>
{
    public Task<int> GetLectureLikeCount(Expression<Func<LectureLike, bool>> filter);
}
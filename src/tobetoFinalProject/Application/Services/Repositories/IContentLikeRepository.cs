using Domain.Entities;
using Core.Persistence.Repositories;
using System.Linq.Expressions;

namespace Application.Services.Repositories;

public interface IContentLikeRepository : IAsyncRepository<ContentLike, Guid>, IRepository<ContentLike, Guid>
{
    public int GetContentLikeCount(Expression<Func<ContentLike, bool>> filter);
}
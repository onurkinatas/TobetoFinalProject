using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IContentLikeRepository : IAsyncRepository<ContentLike, Guid>, IRepository<ContentLike, Guid>
{
}
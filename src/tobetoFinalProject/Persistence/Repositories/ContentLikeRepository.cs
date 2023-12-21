using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ContentLikeRepository : EfRepositoryBase<ContentLike, Guid, BaseDbContext>, IContentLikeRepository
{
    public ContentLikeRepository(BaseDbContext context) : base(context)
    {
    }
}
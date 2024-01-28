using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;
using System.Linq.Expressions;

namespace Persistence.Repositories;

public class ContentLikeRepository : EfRepositoryBase<ContentLike, Guid, BaseDbContext>, IContentLikeRepository
{
    public ContentLikeRepository(BaseDbContext context) : base(context)
    {
    }
    public int GetContentLikeCount(Expression<Func<ContentLike, bool>> filter) => Context.Set<ContentLike>().Where(e => e.DeletedDate == null).Where(filter).ToList().Count;
}
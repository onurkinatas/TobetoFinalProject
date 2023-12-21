using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ContentTagRepository : EfRepositoryBase<ContentTag, Guid, BaseDbContext>, IContentTagRepository
{
    public ContentTagRepository(BaseDbContext context) : base(context)
    {
    }
}
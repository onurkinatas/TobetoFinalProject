using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class TagRepository : EfRepositoryBase<Tag, Guid, BaseDbContext>, ITagRepository
{
    public TagRepository(BaseDbContext context) : base(context)
    {
    }
}
using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class SocialMediaRepository : EfRepositoryBase<SocialMedia, Guid, BaseDbContext>, ISocialMediaRepository
{
    public SocialMediaRepository(BaseDbContext context) : base(context)
    {
    }
}
using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class SubTypeRepository : EfRepositoryBase<SubType, Guid, BaseDbContext>, ISubTypeRepository
{
    public SubTypeRepository(BaseDbContext context) : base(context)
    {
    }
}
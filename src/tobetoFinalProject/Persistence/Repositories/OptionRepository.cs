using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class OptionRepository : EfRepositoryBase<Option, int, BaseDbContext>, IOptionRepository
{
    public OptionRepository(BaseDbContext context) : base(context)
    {
    }
}
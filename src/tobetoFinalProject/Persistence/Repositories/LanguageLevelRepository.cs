using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class LanguageLevelRepository : EfRepositoryBase<LanguageLevel, Guid, BaseDbContext>, ILanguageLevelRepository
{
    public LanguageLevelRepository(BaseDbContext context) : base(context)
    {
    }
}
using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class LectureCompletionConditionRepository : EfRepositoryBase<LectureCompletionCondition, Guid, BaseDbContext>, ILectureCompletionConditionRepository
{
    public LectureCompletionConditionRepository(BaseDbContext context) : base(context)
    {
    }
}
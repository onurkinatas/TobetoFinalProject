using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ClassSurveyRepository : EfRepositoryBase<ClassSurvey, Guid, BaseDbContext>, IClassSurveyRepository
{
    public ClassSurveyRepository(BaseDbContext context) : base(context)
    {
    }
}
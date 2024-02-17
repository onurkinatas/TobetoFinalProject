using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class GeneralQuizRepository : EfRepositoryBase<GeneralQuiz, int, BaseDbContext>, IGeneralQuizRepository
{
    public GeneralQuizRepository(BaseDbContext context) : base(context)
    {
    }
}
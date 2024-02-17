using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ClassQuizRepository : EfRepositoryBase<ClassQuiz, int, BaseDbContext>, IClassQuizRepository
{
    public ClassQuizRepository(BaseDbContext context) : base(context)
    {
    }
}
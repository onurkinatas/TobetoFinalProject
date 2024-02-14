using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class QuizQuestionRepository : EfRepositoryBase<QuizQuestion, int, BaseDbContext>, IQuizQuestionRepository
{
    public QuizQuestionRepository(BaseDbContext context) : base(context)
    {
    }
}
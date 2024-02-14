using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class QuestionOptionRepository : EfRepositoryBase<QuestionOption, int, BaseDbContext>, IQuestionOptionRepository
{
    public QuestionOptionRepository(BaseDbContext context) : base(context)
    {
    }
}
using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class StudentQuizOptionRepository : EfRepositoryBase<StudentQuizOption, int, BaseDbContext>, IStudentQuizOptionRepository
{
    public StudentQuizOptionRepository(BaseDbContext context) : base(context)
    {
    }
}
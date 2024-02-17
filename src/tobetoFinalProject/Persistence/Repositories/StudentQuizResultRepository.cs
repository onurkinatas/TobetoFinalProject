using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class StudentQuizResultRepository : EfRepositoryBase<StudentQuizResult, int, BaseDbContext>, IStudentQuizResultRepository
{
    public StudentQuizResultRepository(BaseDbContext context) : base(context)
    {
    }
}
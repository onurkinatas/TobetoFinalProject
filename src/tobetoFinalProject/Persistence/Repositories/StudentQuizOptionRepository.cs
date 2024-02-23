using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;
using System.Linq.Expressions;

namespace Persistence.Repositories;

public class StudentQuizOptionRepository : EfRepositoryBase<StudentQuizOption, int, BaseDbContext>, IStudentQuizOptionRepository
{
    public StudentQuizOptionRepository(BaseDbContext context) : base(context)
    {
    }
    public async Task<List<StudentQuizOption>> GetAll(Expression<Func<StudentQuizOption, bool>> filter = null)
    {
        return filter == null ? Context.Set<StudentQuizOption>().ToList()
            : Context.Set<StudentQuizOption>().Where(e => e.DeletedDate == null).Where(filter).ToList();
    }
}
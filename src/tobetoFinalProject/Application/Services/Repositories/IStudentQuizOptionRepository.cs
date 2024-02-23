using Domain.Entities;
using Core.Persistence.Repositories;
using System.Linq.Expressions;

namespace Application.Services.Repositories;

public interface IStudentQuizOptionRepository : IAsyncRepository<StudentQuizOption, int>, IRepository<StudentQuizOption, int>
{
    Task<List<StudentQuizOption>> GetAll(Expression<Func<StudentQuizOption, bool>> filter = null);
}
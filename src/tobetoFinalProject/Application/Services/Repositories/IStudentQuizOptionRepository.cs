using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IStudentQuizOptionRepository : IAsyncRepository<StudentQuizOption, int>, IRepository<StudentQuizOption, int>
{
}
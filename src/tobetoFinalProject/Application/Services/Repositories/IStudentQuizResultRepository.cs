using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IStudentQuizResultRepository : IAsyncRepository<StudentQuizResult, int>, IRepository<StudentQuizResult, int>
{
}
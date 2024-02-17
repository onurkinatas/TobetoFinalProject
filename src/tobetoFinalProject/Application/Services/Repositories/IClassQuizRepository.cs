using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IClassQuizRepository : IAsyncRepository<ClassQuiz, int>, IRepository<ClassQuiz, int>
{
}
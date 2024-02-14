using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IQuizRepository : IAsyncRepository<Quiz, int>, IRepository<Quiz, int>
{
}
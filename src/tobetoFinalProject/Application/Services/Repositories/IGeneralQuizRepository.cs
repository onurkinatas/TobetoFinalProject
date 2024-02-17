using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IGeneralQuizRepository : IAsyncRepository<GeneralQuiz, int>, IRepository<GeneralQuiz, int>
{
}
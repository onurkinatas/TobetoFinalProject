using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IQuizQuestionRepository : IAsyncRepository<QuizQuestion, int>, IRepository<QuizQuestion, int>
{
}
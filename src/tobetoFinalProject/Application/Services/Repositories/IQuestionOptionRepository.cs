using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IQuestionOptionRepository : IAsyncRepository<QuestionOption, int>, IRepository<QuestionOption, int>
{
}
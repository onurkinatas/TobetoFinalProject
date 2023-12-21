using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IClassSurveyRepository : IAsyncRepository<ClassSurvey, Guid>, IRepository<ClassSurvey, Guid>
{
}
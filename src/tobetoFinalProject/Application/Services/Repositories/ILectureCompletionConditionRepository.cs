using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ILectureCompletionConditionRepository : IAsyncRepository<LectureCompletionCondition, Guid>, IRepository<LectureCompletionCondition, Guid>
{
}
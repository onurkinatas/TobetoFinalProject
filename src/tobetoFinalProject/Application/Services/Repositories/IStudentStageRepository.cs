using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IStudentStageRepository : IAsyncRepository<StudentStage, Guid>, IRepository<StudentStage, Guid>
{
}
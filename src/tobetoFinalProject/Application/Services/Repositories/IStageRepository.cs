using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IStageRepository : IAsyncRepository<Stage, Guid>, IRepository<Stage, Guid>
{
}
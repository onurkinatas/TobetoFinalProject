using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IAppealRepository : IAsyncRepository<Appeal, Guid>, IRepository<Appeal, Guid>
{
}
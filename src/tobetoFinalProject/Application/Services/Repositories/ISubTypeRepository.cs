using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ISubTypeRepository : IAsyncRepository<SubType, Guid>, IRepository<SubType, Guid>
{
}
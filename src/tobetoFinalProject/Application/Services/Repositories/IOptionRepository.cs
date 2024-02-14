using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IOptionRepository : IAsyncRepository<Option, int>, IRepository<Option, int>
{
}
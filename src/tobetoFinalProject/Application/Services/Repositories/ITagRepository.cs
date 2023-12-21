using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ITagRepository : IAsyncRepository<Tag, Guid>, IRepository<Tag, Guid>
{
}
using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IContentRepository : IAsyncRepository<Content, Guid>, IRepository<Content, Guid>
{
}
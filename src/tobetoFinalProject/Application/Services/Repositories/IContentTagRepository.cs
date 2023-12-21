using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IContentTagRepository : IAsyncRepository<ContentTag, Guid>, IRepository<ContentTag, Guid>
{
}
using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IContentCategoryRepository : IAsyncRepository<ContentCategory, Guid>, IRepository<ContentCategory, Guid>
{
}
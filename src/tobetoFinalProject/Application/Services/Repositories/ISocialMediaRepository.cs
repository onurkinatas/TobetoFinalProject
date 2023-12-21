using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ISocialMediaRepository : IAsyncRepository<SocialMedia, Guid>, IRepository<SocialMedia, Guid>
{
}
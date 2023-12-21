using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IClassAnnouncementRepository : IAsyncRepository<ClassAnnouncement, Guid>, IRepository<ClassAnnouncement, Guid>
{
}
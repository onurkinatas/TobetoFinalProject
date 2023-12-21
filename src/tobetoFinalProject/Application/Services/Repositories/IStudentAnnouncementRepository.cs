using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IStudentAnnouncementRepository : IAsyncRepository<StudentAnnouncement, Guid>, IRepository<StudentAnnouncement, Guid>
{
}
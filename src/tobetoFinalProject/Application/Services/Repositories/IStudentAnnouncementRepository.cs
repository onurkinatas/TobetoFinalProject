using Domain.Entities;
using Core.Persistence.Repositories;
using System.Linq.Expressions;

namespace Application.Services.Repositories;

public interface IStudentAnnouncementRepository : IAsyncRepository<StudentAnnouncement, Guid>, IRepository<StudentAnnouncement, Guid>
{
    public ICollection<StudentAnnouncement> GetAllWithoutPaginate(Expression<Func<StudentAnnouncement, bool>> filter = null);
}
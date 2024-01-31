using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;
using System.Linq.Expressions;

namespace Persistence.Repositories;

public class StudentAnnouncementRepository : EfRepositoryBase<StudentAnnouncement, Guid, BaseDbContext>, IStudentAnnouncementRepository
{
    public StudentAnnouncementRepository(BaseDbContext context) : base(context)
    {
    }
    public List<StudentAnnouncement> GetAllWithoutPaginate(Expression<Func<StudentAnnouncement, bool>> filter = null)
    {
        return filter == null ? Context.Set<StudentAnnouncement>().ToList()
            : Context.Set<StudentAnnouncement>().Where(e => e.DeletedDate == null).Where(filter).ToList();
    }
}
using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class StudentAnnouncementRepository : EfRepositoryBase<StudentAnnouncement, Guid, BaseDbContext>, IStudentAnnouncementRepository
{
    public StudentAnnouncementRepository(BaseDbContext context) : base(context)
    {
    }
}
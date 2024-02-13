using Domain.Entities;
using Core.Persistence.Repositories;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services.Repositories;

public interface IStudentAnnouncementRepository : IAsyncRepository<StudentAnnouncement, Guid>, IRepository<StudentAnnouncement, Guid>
{
    public Task<List<StudentAnnouncement>> GetAllWithoutPaginate(Expression<Func<StudentAnnouncement, bool>> filter = null, Func<IQueryable<StudentAnnouncement>, IIncludableQueryable<StudentAnnouncement, object>>? include = null);
    public Task<List<StudentAnnouncement>> GetAll(Expression<Func<StudentAnnouncement, bool>> filter = null);
}
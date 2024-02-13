using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Core.Persistence.Paging;
using System.Threading;

namespace Persistence.Repositories;

public class StudentAnnouncementRepository : EfRepositoryBase<StudentAnnouncement, Guid, BaseDbContext>, IStudentAnnouncementRepository
{
    public StudentAnnouncementRepository(BaseDbContext context) : base(context)
    {
    }
    public async Task<List<StudentAnnouncement>> GetAllWithoutPaginate(Expression<Func<StudentAnnouncement, bool>> filter = null, Func<IQueryable<StudentAnnouncement>, IIncludableQueryable<StudentAnnouncement, object>>? include = null)
    {
        IQueryable<StudentAnnouncement> queryable = Query();
        if (include!=null)
            queryable = include(queryable);
        if (filter!=null)
            queryable = queryable.Where(filter);

        return queryable.ToList();
    }
    public async Task<List<StudentAnnouncement>> GetAll(Expression<Func<StudentAnnouncement, bool>> filter = null)
    {
        return filter == null ? Context.Set<StudentAnnouncement>().ToList()
            : Context.Set<StudentAnnouncement>().Where(e => e.DeletedDate == null).Where(filter).ToList();
    }
}
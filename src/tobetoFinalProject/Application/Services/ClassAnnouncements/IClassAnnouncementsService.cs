using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ClassAnnouncements;

public interface IClassAnnouncementsService
{
    Task<ClassAnnouncement?> GetAsync(
        Expression<Func<ClassAnnouncement, bool>> predicate,
        Func<IQueryable<ClassAnnouncement>, IIncludableQueryable<ClassAnnouncement, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<ClassAnnouncement>?> GetListAsync(
        Expression<Func<ClassAnnouncement, bool>>? predicate = null,
        Func<IQueryable<ClassAnnouncement>, IOrderedQueryable<ClassAnnouncement>>? orderBy = null,
        Func<IQueryable<ClassAnnouncement>, IIncludableQueryable<ClassAnnouncement, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<ClassAnnouncement> AddAsync(ClassAnnouncement classAnnouncement);
    Task<ClassAnnouncement> UpdateAsync(ClassAnnouncement classAnnouncement);
    Task<ClassAnnouncement> DeleteAsync(ClassAnnouncement classAnnouncement, bool permanent = false);
}

using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.StudentAnnouncements;

public interface IStudentAnnouncementsService
{
    Task<StudentAnnouncement?> GetAsync(
        Expression<Func<StudentAnnouncement, bool>> predicate,
        Func<IQueryable<StudentAnnouncement>, IIncludableQueryable<StudentAnnouncement, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<StudentAnnouncement>?> GetListAsync(
        Expression<Func<StudentAnnouncement, bool>>? predicate = null,
        Func<IQueryable<StudentAnnouncement>, IOrderedQueryable<StudentAnnouncement>>? orderBy = null,
        Func<IQueryable<StudentAnnouncement>, IIncludableQueryable<StudentAnnouncement, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<StudentAnnouncement> AddAsync(StudentAnnouncement studentAnnouncement);
    Task<StudentAnnouncement> UpdateAsync(StudentAnnouncement studentAnnouncement);
    Task<StudentAnnouncement> DeleteAsync(StudentAnnouncement studentAnnouncement, bool permanent = false);
}

using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ContentInstructors;

public interface IContentInstructorsService
{
    Task<ContentInstructor?> GetAsync(
        Expression<Func<ContentInstructor, bool>> predicate,
        Func<IQueryable<ContentInstructor>, IIncludableQueryable<ContentInstructor, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<ContentInstructor>?> GetListAsync(
        Expression<Func<ContentInstructor, bool>>? predicate = null,
        Func<IQueryable<ContentInstructor>, IOrderedQueryable<ContentInstructor>>? orderBy = null,
        Func<IQueryable<ContentInstructor>, IIncludableQueryable<ContentInstructor, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<ContentInstructor> AddAsync(ContentInstructor contentInstructor);
    Task<ContentInstructor> UpdateAsync(ContentInstructor contentInstructor);
    Task<ContentInstructor> DeleteAsync(ContentInstructor contentInstructor, bool permanent = false);
}

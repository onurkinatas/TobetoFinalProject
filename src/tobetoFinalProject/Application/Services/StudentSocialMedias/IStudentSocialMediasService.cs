using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.StudentSocialMedias;

public interface IStudentSocialMediasService
{
    Task<StudentSocialMedia?> GetAsync(
        Expression<Func<StudentSocialMedia, bool>> predicate,
        Func<IQueryable<StudentSocialMedia>, IIncludableQueryable<StudentSocialMedia, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<StudentSocialMedia>?> GetListAsync(
        Expression<Func<StudentSocialMedia, bool>>? predicate = null,
        Func<IQueryable<StudentSocialMedia>, IOrderedQueryable<StudentSocialMedia>>? orderBy = null,
        Func<IQueryable<StudentSocialMedia>, IIncludableQueryable<StudentSocialMedia, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<StudentSocialMedia> AddAsync(StudentSocialMedia studentSocialMedia);
    Task<StudentSocialMedia> UpdateAsync(StudentSocialMedia studentSocialMedia);
    Task<StudentSocialMedia> DeleteAsync(StudentSocialMedia studentSocialMedia, bool permanent = false);
}
